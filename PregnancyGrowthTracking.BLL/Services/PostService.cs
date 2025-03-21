using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Amazon;
using Azure;
using Tag = PregnancyGrowthTracking.DAL.Entities.Tag;


namespace PregnancyGrowthTracking.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IUserRepository _userRepo;
        private readonly IPostRepository _postRepo;
        private readonly IPostTagRepository _postTagRepo;
        private readonly ITagRepository _tagRepo;
        private readonly IConfiguration _configuration;

        public PostService(IUserRepository userRepo, IPostRepository postRepo, IPostTagRepository postTagRepo, ITagRepository tagRepo, PregnancyGrowthTrackingDbContext context, IConfiguration configuration)
        {
            _userRepo = userRepo;
            _postRepo = postRepo;
            _postTagRepo = postTagRepo;
            _tagRepo = tagRepo;
            _configuration = configuration;
        }


        public async Task<List<PostDto>> GetAllPostWithIdAsync()
        {
            List<Post> listPost = await _postRepo.GetAllPostWithTagAsync();

            // vỡi mỗi blog có trong listBlog sẽ tương ứng với 1 blog trong listBlogDTO
            List<PostDto> listPostDto = listPost.Select(p => new PostDto
            {
                Id = p.PostId,
                Title = p.Title,
                Body = p.Body,
                PostImageUrl = p.PostImageUrl,
                PostTags = p.PostTags.Select(pt => new PostDto.PostTagDTO
                {
                    TagName = pt.Tag.TagName
                }).ToList()
            }).ToList();

            return listPostDto;
        }

        public async Task UpdatePostAsync(UpdatePostDto postDTO)
        {
            try
            {
                // Kiểm tra số lượng tag khi update
                if (postDTO.Tags != null && postDTO.Tags.Count > 2)
                {
                    throw new ArgumentException("Chỉ được phép có tối đa 2 tags");
                }

                // 1. Lấy post hiện tại
                var existingPost = await _postRepo.GetPostByIdAsync(postDTO.Id);
                if (existingPost == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy post với id: {postDTO.Id}");
                }

                // 2. Chỉ cập nhật các trường được điền
                if (postDTO.Title != null)
                {
                    existingPost.Title = postDTO.Title;
                }
                if (postDTO.Body != null)
                {
                    existingPost.Body = postDTO.Body;
                }

                // 3. Cập nhật post
                await _postRepo.UpdatePostAsync(existingPost);

                // 4. Xử lý tags nếu có thay đổi
                if (postDTO.Tags != null)
                {
                    // 4.1 Xóa tất cả tags cũ
                    foreach (var postTag in existingPost.PostTags.ToList())
                    {
                        await _postTagRepo.RemovePostTagAsyns(postTag);
                    }

                    // 4.2 Thêm tags mới
                    foreach (var tagDto in postDTO.Tags)
                    {
                        // Kiểm tra và thêm tag nếu chưa tồn tại
                        var existingTag = await _tagRepo.GetTagByName(tagDto.TagName);
                        DAL.Entities.Tag tag;

                        if (existingTag == null)
                        {
                            tag = new DAL.Entities.Tag { TagName = tagDto.TagName };
                            await _tagRepo.AddTagAsync(tag);
                        }
                        else
                        {
                            tag = existingTag;
                        }

                        // Tạo liên kết PostTag mới
                        var postTag = new PostTag
                        {
                            PostId = existingPost.PostId,
                            TagId = tag.TagId
                        };
                        await _postTagRepo.AddPostTagAsync(postTag);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdatePostTagAsync(UpdatePostDto postDTO)
        {
            Post existingPost = await _postRepo.GetPostByIdAsync(postDTO.Id);
            DAL.Entities.Tag? currentTag = null;
            List<PostTag> currentListPostTag = new();

            foreach (var postTag in postDTO.Tags.ToList())
            {
                if (postTag.TagName == "")
                {
                    return;
                }

                currentTag = await _tagRepo.GetTagByName(postTag.TagName);

                currentListPostTag.Add(new PostTag()
                {
                    PostId = existingPost.PostId,
                    TagId = currentTag.TagId
                });
            }

            if (currentListPostTag != null)
            {
                foreach (PostTag postTag in existingPost.PostTags.ToList())
                {
                    await _postTagRepo.RemovePostTagAsyns(postTag);
                }
            }

            foreach (PostTag? postTag in currentListPostTag)
            {
                if (postTag != null)
                {
                    await _postTagRepo.AddPostTagAsync(postTag);
                }
            }
        }

        public async Task AddPostAsync(int userId, CreatePostDto postDTO)
        {
            if (string.IsNullOrEmpty(postDTO.Title) || string.IsNullOrEmpty(postDTO.Body))
            {
                throw new ArgumentException("Title và Body không được để trống");
            }

            if (postDTO.CreatePostTag != null && postDTO.CreatePostTag.Count > 2)
            {
                throw new ArgumentException("Chỉ được phép thêm tối đa 2 tags");
            }

            var userExists = await _userRepo.GetUserByIdAsync(userId);
            if (userExists == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy user với id: {userId}");
            }

            // Upload ảnh nếu có
            string? postImageUrl = null;
            if (postDTO.PostImage != null)
            {
                postImageUrl = await UploadPhotoToS3(postDTO.PostImage);
            }

            // Tạo Post mới
            Post post = new Post
            {
                Title = postDTO.Title,
                Body = postDTO.Body,
                UserId = userId,
                CreatedDate = DateTime.UtcNow,
                IsActive = true,
                PostImageUrl = postImageUrl
            };

            await _postRepo.AddPostAsync(post);

            // Xử lý tags nếu có
            if (postDTO.CreatePostTag != null && postDTO.CreatePostTag.Any())
            {
                foreach (var tagDto in postDTO.CreatePostTag)
                {
                    // Chuẩn hóa tên tag
                    string normalizedTagName = tagDto.TagName.Trim().ToLower();

                    // Kiểm tra tag đã tồn tại chưa
                    Tag? existingTag = await _tagRepo.GetTagByName(normalizedTagName);
                    Tag currentTag;

                    // Nếu tag chưa tồn tại, thêm mới vào bảng Tag
                    if (existingTag == null)
                    {
                        currentTag = new Tag { TagName = normalizedTagName };
                        await _tagRepo.AddTagAsync(currentTag);
                    }
                    else
                    {
                        currentTag = existingTag;
                    }

                    // Tạo liên kết trong bảng PostTag
                    var postTag = new PostTag
                    {
                        PostId = post.PostId,
                        TagId = currentTag.TagId
                    };

                    // Lưu vào bảng PostTag
                    await _postTagRepo.AddPostTagAsync(postTag);
                }
            }
        }

        public async Task DeletePostAsync(int postID)
        {
            if (postID < 0)
            {
                throw new ArgumentException("Post Id phải lớn hơn 1");
            }

            Post currentPost = await _postRepo.GetPostByIdAsync(postID);

            if (currentPost == null)
            {
                throw new Exception();
            }

            foreach (PostTag postTag in currentPost.PostTags.ToList())
            {
                await _postTagRepo.RemovePostTagAsyns(postTag);
            }

            await _postRepo.DeletePostAsync(currentPost);
        }

        public async Task<PostDto> GetPostByIdAsync(int postId)
        {
            var post = await _postRepo.GetPostByIdAsync(postId);

            // Kiểm tra post tồn tại và đang active
            if (post == null || !post.IsActive)
            {
                throw new KeyNotFoundException($"Không tìm thấy bài post với id: {postId}");
            }

            return new PostDto
            {
                Id = post.PostId,
                Title = post.Title,
                Body = post.Body,
                PostImageUrl = post.PostImageUrl,
                CreatedDate = post.CreatedDate,
                UserId = post.UserId,
                PostTags = post.PostTags.Select(pt => new PostDto.PostTagDTO
                {
                    TagName = pt.Tag.TagName
                }).ToList()
            };
        }

        //public async Task<string> UploadPhotoAsync(int postId, IFormFile file)
        //{
        //    if (file == null)
        //    {
        //        throw new ArgumentNullException(nameof(file), "File is required.");
        //    }

        //    const long maxFileSize = 10485760; // 10MB
        //    if (file.Length > maxFileSize)
        //    {
        //        throw new InvalidOperationException($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
        //    }


        //    var post = await _postRepo.GetPostByIdAsync(postId);
        //    if (post == null)
        //    {
        //        throw new KeyNotFoundException("Post not found.");
        //    }

        //    // Upload ảnh mới lên S3
        //    var photoUrl = await UploadPhotoToS3(file);

        //    // Cập nhật đường link ảnh mới vào database
        //    post.PostImageUrl = photoUrl;
        //    await _blogRepo.UpdateBlogAsync(blog);

        //    return photoUrl;
        //}

        //public async Task<string> ReplacePhotoAsync(int blogId, IFormFile file)
        //{
        //    if (file == null)
        //    {
        //        throw new ArgumentNullException(nameof(file), "File is required.");
        //    }

        //    const long maxFileSize = 10485760; // 10MB
        //    if (file.Length > maxFileSize)
        //    {
        //        throw new InvalidOperationException($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
        //    }

        //    // Lấy thông tin blog hiện tại
        //    var blog = await _blogRepo.GetBlogByIdAsync(blogId);
        //    if (blog == null)
        //    {
        //        throw new KeyNotFoundException("Blog not found.");
        //    }

        //    // Xóa đường link ảnh cũ trong database
        //    blog.BlogImageUrl = null;
        //    await _blogRepo.UpdateBlogAsync(blog);

        //    // Upload ảnh mới lên S3
        //    var newPhotoUrl = await UploadPhotoToS3(file);

        //    // Cập nhật đường link ảnh mới vào database
        //    blog.BlogImageUrl = newPhotoUrl;
        //    await _blogRepo.UpdateBlogAsync(blog);

        //    return newPhotoUrl;
        //}

        //private async Task<string> UploadPhotoToS3(IFormFile file)
        //{
        //    var bucketName = _configuration["Blog:BucketName"];
        //    var accessKey = _configuration["Blog:AccessKey"];
        //    var secretKey = _configuration["Blog:SecretKey"];
        //    var region = _configuration["Blog:Region"];

        //    using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

        //    var fileKey = $"blog/{Guid.NewGuid()}_{file.FileName}";

        //    using var stream = file.OpenReadStream();
        //    var request = new PutObjectRequest
        //    {
        //        BucketName = bucketName,
        //        Key = fileKey,
        //        InputStream = stream,
        //        ContentType = file.ContentType
        //    };

        //    await s3Client.PutObjectAsync(request);

        //    return $"https://{bucketName}.s3.amazonaws.com/{fileKey}";
        //}

        //Task IBlogService.UploadPhotoAsync(int blogId, IFormFile file)
        //{
        //    throw new NotImplementedException();
        //}

        //Task IBlogService.ReplacePhotoAsync(int blogId, IFormFile file)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task RemoveTagFromPostAsync(int postId, string tagName)
        {
            // 1. Kiểm tra post tồn tại
            var post = await _postRepo.GetPostByIdAsync(postId);
            if (post == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy post với id: {postId}");
            }

            // 2. Tìm tag cần xóa
            var tag = await _tagRepo.GetTagByName(tagName);
            if (tag == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy tag: {tagName}");
            }

            // 3. Tìm và xóa liên kết PostTag
            PostTag postTag = await _postTagRepo.GetPostTagByPostIdAndTagId(postId, tag.TagId);

            if (postTag == null)
            {
                throw new KeyNotFoundException($"Post không có tag: {tagName}");
            }

            await _postTagRepo.RemovePostTagAsyns(postTag);
        }

        public async Task DeactivatePostAsync(int postID)
        {
            try
            {
                if (postID < 0)
                {
                    throw new ArgumentException("Post Id phải lớn hơn 0");
                }

                // Lấy post hiện tại
                Post currentPost = await _postRepo.GetPostByIdAsync(postID);
                if (currentPost == null)
                {
                    throw new KeyNotFoundException($"Không tìm thấy post với id: {postID}");
                }

                // Đổi IsActive thành false thay vì xóa
                currentPost.IsActive = false;
                await _postRepo.UpdatePostAsync(currentPost);

            }
            catch
            {
                throw;
            }
        }
        private async Task<string> UploadPhotoToS3(IFormFile file)
        {
            var bucketName = _configuration["Post:BucketName"];
            var accessKey = _configuration["Post:AccessKey"];
            var secretKey = _configuration["Post:SecretKey"];
            var region = _configuration["Post:Region"];

            using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            var fileKey = $"post_images/{Guid.NewGuid()}_{file.FileName}"; // Thay đổi thư mục lưu ảnh

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileKey,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await s3Client.PutObjectAsync(request);

            return $"https://{bucketName}.s3.amazonaws.com/{fileKey}"; // Trả về PostImageUrl
        }

    }
}
