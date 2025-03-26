using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Amazon;


namespace PregnancyGrowthTracking.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;
        private readonly IBlogCateRepository _blogCateRepo;
        private readonly ICateRepository _cateRepo;
        private readonly PregnancyGrowthTrackingDbContext _context;
        private readonly IConfiguration _configuration;

        public BlogService(IBlogRepository blogRepo, IBlogCateRepository blogCateRepo, ICateRepository cateRepo, PregnancyGrowthTrackingDbContext context, IConfiguration configuration)
        {
            _blogRepo = blogRepo;
            _blogCateRepo = blogCateRepo;
            _cateRepo = cateRepo;
            _context = context;
            _configuration = configuration;
        }

        // sử dụng lớp BlogDTO ở đây để tránh circular reference cũng như quản lý/giới hạn  thông tin mà client nhận được để tránh lộ thông tin nhạy cảm
        public async Task<List<BlogDTO>> GetAllBlogWithCateAsync()
        {
            List<Blog> listBlog = await _blogRepo.GetAllBlogWithCateAsync();

            // vỡi mỗi blog có trong listBlog sẽ tương ứng với 1 blog trong listBlogDTO
            List<BlogDTO> listBlogDTO = listBlog.Select(b => new BlogDTO
            {
                Id = b.BlogId,
                Title = b.Title,
                Body = b.Body,
                BlogImageUrl = b.BlogImageUrl,
                Categories = b.BlogCates.Select(bc => new BlogDTO.BlogCategoryDTO
                {
                    CategoryName = bc.Category.CategoryName
                }).ToList()
            }).ToList();

            return listBlogDTO;
        }

        public async Task UpdateBlogAsync(UpdateBlogDTO blogDTO)
        {
            Blog existingBlog = await _blogRepo.GetBlogByIdAsync(blogDTO.Id);

            if (existingBlog == null)
            {
                return;
            }

            existingBlog.Title = blogDTO.Title ?? existingBlog.Title;
            existingBlog.Body = blogDTO.Body ?? existingBlog.Body;


            await _blogRepo.UpdateBlogAsync(existingBlog);

            if (blogDTO.Categories != null)
            {

                await UpdateBlogCateAsync(blogDTO);

            }
        }

        public async Task UpdateBlogCateAsync(UpdateBlogDTO blogDTO)
        {
            Blog existingBlog = await _blogRepo.GetBlogByIdAsync(blogDTO.Id);
            Category? currentCate = null;
            List<BlogCate> currentListBlogCate = new();

            foreach (var blogCate in blogDTO.Categories.ToList())
            {
                if (blogCate.CategoryName == "")
                {
                    return;
                }

                currentCate = await _cateRepo.GetCategoryByName(blogCate.CategoryName);

                currentListBlogCate.Add(new BlogCate()
                {
                    BlogId = existingBlog.BlogId,
                    CategoryId = currentCate.CategoryId
                });
            }

            if (currentListBlogCate != null)
            {
                foreach (BlogCate blogCate in existingBlog.BlogCates.ToList())
                {
                    await _blogCateRepo.RemoveBlogCateAsyns(blogCate);
                }
            }

            foreach (BlogCate? blogCate in currentListBlogCate)
            {
                if (blogCate != null)
                {
                    await _blogCateRepo.AddBlogCateAsync(blogCate);
                }
            }
        }

        public async Task AddBlogAsync(CreateBlogDTO blogDTO)
        {
            Blog blog = new Blog
            {
                Title = blogDTO.Title,
                Body = blogDTO.Body,
            };

            await _blogRepo.AddBlogAsync(blog);

            Blog newBlog = await _blogRepo.GetBlogByTitleAndBodyAsync(blog.Title, blog.Body);

            foreach (var blogcate in blogDTO.CreateBlogCategories)
            {
                Category newCate = await _cateRepo.GetCategoryByName(blogcate.CategoryName);
                BlogCate newBlogCate = new BlogCate
                {
                    BlogId = newBlog.BlogId,
                    CategoryId = newCate.CategoryId
                };

                await _blogCateRepo.AddBlogCateAsync(newBlogCate);
            }
        }

        public async Task DeleteBlogAsync(int blogID)
        {
            if (blogID < 0)
            {
                throw new ArgumentException("Blog Id phải lớn hơn 1");
            }

            Blog currentBlog = await _blogRepo.GetBlogByIdAsync(blogID);

            if (currentBlog == null)
            {
                throw new Exception();
            }

            foreach (BlogCate blogCate in currentBlog.BlogCates.ToList())
            {
                await _blogCateRepo.RemoveBlogCateAsyns(blogCate);
            }

            await _blogRepo.DeleteBlogAsync(currentBlog);
        }

        public async Task<BlogDTO> GetBlogByIdAsync(int blogId)
        {
            var blog = await _blogRepo.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                return null;
            }

            return new BlogDTO
            {
                Id = blog.BlogId,
                Title = blog.Title,
                Body = blog.Body,
                //BlogImageUrl = blog.BlogImageUrl,
                Categories = blog.BlogCates.Select(bc => new BlogDTO.BlogCategoryDTO
                {
                    CategoryName = bc.Category.CategoryName
                }).ToList()
            };
        }

        public async Task<string> UploadPhotoAsync(int blogId, IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File is required.");
            }

            const long maxFileSize = 10485760; // 10MB
            if (file.Length > maxFileSize)
            {
                throw new InvalidOperationException($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
            }

            // Lấy thông tin blog hiện tại
            var blog = await _blogRepo.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }

            // Upload ảnh mới lên S3
            var photoUrl = await UploadPhotoToS3(file);

            // Cập nhật đường link ảnh mới vào database
            blog.BlogImageUrl = photoUrl;
            await _blogRepo.UpdateBlogAsync(blog);

            return photoUrl;
        }

        public async Task<string> ReplacePhotoAsync(int blogId, IFormFile file)
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file), "File is required.");
            }

            const long maxFileSize = 10485760; // 10MB
            if (file.Length > maxFileSize)
            {
                throw new InvalidOperationException($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
            }

            // Lấy thông tin blog hiện tại
            var blog = await _blogRepo.GetBlogByIdAsync(blogId);
            if (blog == null)
            {
                throw new KeyNotFoundException("Blog not found.");
            }

            // Xóa đường link ảnh cũ trong database
            blog.BlogImageUrl = null;
            await _blogRepo.UpdateBlogAsync(blog);

            // Upload ảnh mới lên S3
            var newPhotoUrl = await UploadPhotoToS3(file);

            // Cập nhật đường link ảnh mới vào database
            blog.BlogImageUrl = newPhotoUrl;
            await _blogRepo.UpdateBlogAsync(blog);

            return newPhotoUrl;
        }

        private async Task<string> UploadPhotoToS3(IFormFile file)
        {
            var bucketName = _configuration["Blog:BucketName"];
            var accessKey = _configuration["Blog:AccessKey"];
            var secretKey = _configuration["Blog:SecretKey"];
            var region = _configuration["Blog:Region"];

            using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            var fileKey = $"blog/{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = fileKey,
                InputStream = stream,
                ContentType = file.ContentType
            };

            await s3Client.PutObjectAsync(request);

            return $"https://{bucketName}.s3.amazonaws.com/{fileKey}";
        }

        Task IBlogService.UploadPhotoAsync(int blogId, IFormFile file)
        {
            throw new NotImplementedException();
        }

        Task IBlogService.ReplacePhotoAsync(int blogId, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDTO>> GetAllCategory()
        {
            var categories = await _cateRepo.GetAllCategory();
            return categories.Select(c => new CategoryDTO
            {
                CategoryName = c.CategoryName
            }).ToList();
        }
    }
}
