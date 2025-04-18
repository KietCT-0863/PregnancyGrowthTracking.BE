﻿using Amazon.S3.Model;
using Amazon.S3;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using Amazon;
using System.Security.Claims;
using System;
using System.Text.Json;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepo;

        public PostController(IPostService postService, IConfiguration configuration, IPostRepository postRepo)
        {
            _postService = postService;
            _configuration = configuration;
            _postRepo = postRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<PostDto>>> GetAllPosts()
        {
            try
            {
                List<PostDto> posts = await _postService.GetAllPostWithIdAsync();

                var result = new
                {
                    posts = posts.Select(p => new ReturnPostDto
                    {
                        Id = p.Id,
                        UserId = p.UserId,
                        Title = p.Title,
                        Body = p.Body,
                        PostImageUrl = p.PostImageUrl,
                        CreatedDate = p.CreatedDate,
                        PostTags = p.PostTags.Select(t => t.TagName).ToList()
                    })
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách post");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id phải lớn hơn 0");
                }

                var post = await _postService.GetPostByIdAsync(id);
                return Ok(post);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("userid/{id}")]
        public async Task<ActionResult<List<PostDto>>> GetPostsByUserId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id phải lớn hơn 0");
                }

                var posts = await _postService.GetPostsByUserIdAsync(id);
                return Ok(posts);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        
        [HttpGet("my-posts")]
        [Authorize]
        public async Task<ActionResult<List<PostDto>>> GetMyPosts()
        {
            try
            {
                var userId = int.Parse(User.FindFirst("UserId")?.Value ?? "0");

                if (userId <= 0)
                {
                    return Unauthorized("Không tìm thấy thông tin UserId trong token.");
                }

                var posts = await _postService.GetPostsByUserIdAsync(userId);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpGet("{postId}/author")]
        public async Task<ActionResult<PostAuthorDto>> GetPostAuthor(int postId)
        {
            try
            {
                if (postId <= 0)
                {
                    return BadRequest("PostId phải lớn hơn 0.");
                }

                var author = await _postService.GetPostAuthorAsync(postId);
                return Ok(author);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi server: {ex.Message}");
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostDto postDTO)
        {
            try
            {
                // 1. Validate Id
                if (postDTO.Id <= 0)
                {
                    return BadRequest(new { message = "Id post không hợp lệ" });
                }

                // 2. Kiểm tra post tồn tại
                var existingPost = await _postRepo.GetPostByIdAsync(postDTO.Id);
                if (existingPost == null)
                {
                    return NotFound(new { message = $"Không tìm thấy post với id: {postDTO.Id}" });
                }

                // 3. Kiểm tra UserId từ token
                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng." });
                }

                // 4. Kiểm tra quyền - chỉ admin hoặc người tạo bài viết mới được sửa
                var isAdmin = User.IsInRole("admin");

                if (!isAdmin && existingPost.UserId != currentUserId)
                {
                    return StatusCode(403, new { message = "Bạn không có quyền sửa bài viết này" });
                }

                // 5. Validate tags
                if (postDTO.Tags != null)
                {
                    // 5.1 Kiểm tra số lượng tags
                    if (postDTO.Tags.Count > 2)
                    {
                        return BadRequest(new { message = "Chỉ được phép có tối đa 2 tags" });
                    }

                    // 5.2 Kiểm tra tag name rỗng
                    if (postDTO.Tags.Any(t => string.IsNullOrWhiteSpace(t.TagName)))
                    {
                        return BadRequest(new { message = "TagName không được để trống" });
                    }
                }

                // 6. Validate title và body
                if (postDTO.Title != null && string.IsNullOrWhiteSpace(postDTO.Title))
                {
                    return BadRequest(new { message = "Title không được để trống" });
                }

                if (postDTO.Body != null && string.IsNullOrWhiteSpace(postDTO.Body))
                {
                    return BadRequest(new { message = "Body không được để trống" });
                }

                // 7. Thực hiện update
                await _postService.UpdatePostAsync(postDTO);
                return Ok("Cập nhật post thành công");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi cập nhật post: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPost([FromForm] CreatePostFormDto formDto, [FromForm] List<string> tagNames)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createPostDTO = new CreatePostDto
                {
                    Title = formDto.Title,
                    Body = formDto.Body,
                    PostImage = formDto.PostImage,
                    CreatePostTag = new List<CreatePostDto.CreatePostTagDTO>()
                };

                // Xử lý tags từ form data
                if (tagNames != null && tagNames.Any())
                {
                    if (tagNames.Count > 2)
                    {
                        return BadRequest(new { message = "Chỉ được phép thêm tối đa 2 tags." });
                    }

                    // Kiểm tra tag name rỗng và chuyển đổi thành CreatePostTagDTO
                    foreach (var tagName in tagNames)
                    {
                        if (string.IsNullOrWhiteSpace(tagName))
                        {
                            return BadRequest(new { message = "TagName không được để trống" });
                        }

                        createPostDTO.CreatePostTag.Add(new CreatePostDto.CreatePostTagDTO
                        {
                            TagName = tagName.Trim()
                        });
                    }
                }

                var userIdClaim = HttpContext.User.FindFirst("UserId");
                if (userIdClaim == null)
                {
                    return Unauthorized(new { message = "Người dùng chưa đăng nhập." });
                }

                var userId = int.Parse(userIdClaim.Value);

                await _postService.AddPostAsync(userId, createPostDTO);
                return Ok("Thêm post thành công");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Có lỗi xảy ra khi thêm post: {ex.Message}");
            }
        }


        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePost(int postID)
        {
            try
            {
                if (postID < 1)
                {
                    return BadRequest(new { message = "Post id là bắt buộc" });
                }

                var existingPost = await _postRepo.GetPostByIdAsync(postID);
                if (existingPost == null)
                {
                    return NotFound(new { message = $"Không tìm thấy post với id: {postID}" });
                }

                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng." });
                }

                var isAdmin = User.IsInRole("admin");

                if (!isAdmin && existingPost.UserId != currentUserId)
                {
                    return StatusCode(403, new { message = "Bạn không có quyền xóa bài viết này" });
                }

                await _postService.DeactivatePostAsync(postID);
                return Ok(new { message = "Xóa post thành công." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Có lỗi xảy ra khi xóa post: {ex.Message}" });
            }
        }


        [HttpDelete("tags")]
        [Authorize]
        public async Task<IActionResult> RemoveTagFromPost(int postId, string tagName)
        {
            try
            {
                var existingPost = await _postRepo.GetPostByIdAsync(postId);
                if (existingPost == null)
                {
                    return NotFound(new { message = $"Không tìm thấy post với id: {postId}" });
                }

                var userIdClaim = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int currentUserId))
                {
                    return Unauthorized(new { message = "Không thể xác định người dùng." });
                }

                // Kiểm tra quyền: chỉ admin hoặc chủ bài viết mới có quyền xóa tag
                var isAdmin = User.IsInRole("admin");

                if (!isAdmin && existingPost.UserId != currentUserId)
                {
                    return StatusCode(403, new { message = "Bạn không có quyền chỉnh sửa bài viết này" });
                }

                await _postService.RemoveTagFromPostAsync(postId, tagName);
                return Ok(new { message = $"Đã xóa tag '{tagName}' khỏi post thành công" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Có lỗi xảy ra khi xóa tag: {ex.Message}" });
            }
        }

        [HttpPost("upload-photo/{postId}")]
        public async Task<IActionResult> UploadPhoto(int postId, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("File is required.");
                }

                const long maxFileSize = 10485760;
                if (file.Length > maxFileSize)
                {
                    return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                }

                var post = await _postRepo.GetPostByIdAsync(postId);
                if (post == null)
                {
                    return NotFound("Post not found.");
                }

                var userId = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                {
                    return Unauthorized("Cannot determine user.");
                }

                if (post.UserId != parsedUserId)
                {
                    return StatusCode(403, "You do not have permission to edit this post.");
                }


                var photoUrl = await UploadPhotoToS3(file);

                post.PostImageUrl = photoUrl;
                await _postRepo.UpdatePostAsync(post);

                return Ok(new { PhotoUrl = photoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the photo: {ex.Message}");
            }
        }

        [HttpPut("replace-photo/{postId}")]
        public async Task<IActionResult> ReplacePhoto(int postId, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("File is required.");
                }

                const long maxFileSize = 10485760;
                if (file.Length > maxFileSize)
                {
                    return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                }

                var post = await _postRepo.GetPostByIdAsync(postId);
                if (post == null)
                {
                    return NotFound("Post not found.");
                }

                var userId = User.FindFirst("UserId")?.Value;
                if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int parsedUserId))
                {
                    return Unauthorized("Cannot determine user.");
                }

                if (post.UserId != parsedUserId)
                {
                    return StatusCode(403, "You do not have permission to edit this post.");
                }

                post.PostImageUrl = null;
                await _postRepo.UpdatePostAsync(post);

                var newPhotoUrl = await UploadPhotoToS3(file);

                post.PostImageUrl = newPhotoUrl;
                await _postRepo.UpdatePostAsync(post);

                return Ok(new { PhotoUrl = newPhotoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while replacing the photo: {ex.Message}");
            }
        }

        private async Task<string> UploadPhotoToS3(IFormFile file)
        {
            var bucketName = _configuration["Post:BucketName"];
            var accessKey = _configuration["Post:AccessKey"];
            var secretKey = _configuration["Post:SecretKey"];
            var region = _configuration["Post:Region"];

            using var s3Client = new AmazonS3Client(accessKey, secretKey, RegionEndpoint.GetBySystemName(region));

            var fileKey = $"post/{Guid.NewGuid()}_{file.FileName}";

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

    }

}