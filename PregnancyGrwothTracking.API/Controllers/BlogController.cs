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
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<List<BlogDTO>>> GetAllBlogs()
        {
            try
            {
                var blogs = await _blogService.GetAllBlogWithCateAsync();
                return Ok(blogs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi lấy danh sách blog");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogDTO blogDTO)
        {
            try
            {
                await _blogService.UpdateBlogAsync(id ,blogDTO);
                return Ok("Cập nhật blog thành công");
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi cập nhật blog");
            }
        }
    }
}
