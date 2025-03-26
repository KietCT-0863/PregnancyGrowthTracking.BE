using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Configuration;
using Amazon;
using Microsoft.AspNetCore.Http;
using System;
using PregnancyGrowthTracking.DAL.Repositories;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;
        private readonly IConfiguration _configuration;
        private readonly IBlogRepository _blogRepo;

        public BlogController(IBlogService blogService, IConfiguration configuration, IBlogRepository blogRepo)
        {
            _blogService = blogService;
            _configuration = configuration;
            _blogRepo = blogRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogDTO>>> GetAllBlogs()
        {
            try
            {
                List<BlogDTO> blogs = await _blogService.GetAllBlogWithCateAsync();

                // Nested Json - Json lồng nhau
                var result = new
                {
                    posts = blogs.Select(b => new ReturnBlogDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Body = b.Body,
                        BlogImageUrl = b.BlogImageUrl,
                        Categories = b.Categories.Select(c => c.CategoryName).ToList()
                    })
                };
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách blog");
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBlog([FromBody] UpdateBlogDTO blogDTO)
        {
            try
            {
                await _blogService.UpdateBlogAsync(blogDTO);
                return Ok("Cập nhật blog thành công");
            }
            catch (ArgumentException e)
            {
                return BadRequest(new { message = e.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi cập nhật blog");
            }
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddBlog([FromBody] CreateBlogDTO createBlogDTO)
        {
            try
            {
                await _blogService.AddBlogAsync(createBlogDTO);
                return Ok("Thêm blog thành công");
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi thêm blog");
            }
        }

        [HttpDelete()]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteBlog(int blogID)
        {
            try
            {
                if (blogID < 1)
                {
                    return BadRequest("Blog id là bắt buộc");
                }

                await _blogService.DeleteBlogAsync(blogID);
                return Ok("Xoá Blog thành công.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi xoá blog");
            }
        }

        [HttpPost("upload-photo/{blogId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UploadPhoto(int blogId, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("File is required.");
                }

                const long maxFileSize = 10485760; // 10MB
                if (file.Length > maxFileSize)
                {
                    return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                }

                // Lấy thông tin blog hiện tại
                var blog = await _blogRepo.GetBlogByIdAsync(blogId);
                if (blog == null)
                {
                    return NotFound("Blog not found.");
                }

                // Upload ảnh mới lên S3
                var photoUrl = await UploadPhotoToS3(file);

                // Cập nhật đường link ảnh mới vào database
                blog.BlogImageUrl = photoUrl;
                await _blogRepo.UpdateBlogAsync(blog);

                return Ok(new { PhotoUrl = photoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the photo: {ex.Message}");
            }
        }

        [HttpPut("replace-photo/{blogId}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ReplacePhoto(int blogId, IFormFile file)
        {
            try
            {
                if (file == null)
                {
                    return BadRequest("File is required.");
                }

                const long maxFileSize = 10485760; // 10MB
                if (file.Length > maxFileSize)
                {
                    return BadRequest($"File {file.FileName} exceeds the maximum allowed size of 10 MB.");
                }

                // Lấy thông tin blog hiện tại
                var blog = await _blogRepo.GetBlogByIdAsync(blogId);
                if (blog == null)
                {
                    return NotFound("Blog not found.");
                }

                // Xóa đường link ảnh cũ trong database
                blog.BlogImageUrl = null;
                await _blogRepo.UpdateBlogAsync(blog);

                // Upload ảnh mới lên S3
                var newPhotoUrl = await UploadPhotoToS3(file);

                // Cập nhật đường link ảnh mới vào database
                blog.BlogImageUrl = newPhotoUrl;
                await _blogRepo.UpdateBlogAsync(blog);

                return Ok(new { PhotoUrl = newPhotoUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while replacing the photo: {ex.Message}");
            }
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

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                List<CategoryDTO> categories = await _blogService.GetAllCategory();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Có lỗi xảy ra khi lấy danh sách categories", error = ex.Message });
            }
        }
    }
}
