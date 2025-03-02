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
                var result = new
                {
                    posts = blogs.Select(b => new ReturnBlogDTO
                    {
                        Id = b.Id,
                        Title = b.Title,
                        Body = b.Body,
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
        public async Task<IActionResult> UpdateBlog([FromBody] BlogDTO blogDTO)
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
        //[Authorize(Roles = "admin")]
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
        public async Task<IActionResult> DeleteBlog(int blogID)
        {
            try
            {
                if (blogID < 1)
                {
                    return BadRequest("Blog id là bắt buộc");
                }

                await _blogService.DeleteBlogAsync(blogID);
                return Ok("Blog deleted successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Có lỗi xảy ra khi xoá blog");
            }
        }
    }
}
