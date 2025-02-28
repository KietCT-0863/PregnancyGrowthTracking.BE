using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BlogDTO>>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogWithCateAsync();

                // Apply Nested JSON
                var resutl = new
                {
                    posts = blogs.Select(b => new ReturnBlogDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Body = b.Body,
                        Categories = b.Categories.Select(c => c.CategoryName).ToList()
                    })
                };
                return Ok(resutl);
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách blog");
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBlog([FromBody] BlogDTO blogDTO)
        {
            try
            {
                await _blogService.UpdateBlogAsync(blogDTO);
                return Ok("Cập nhật blog thành công");
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi cập nhật blog");
            }
        }
    }
}
