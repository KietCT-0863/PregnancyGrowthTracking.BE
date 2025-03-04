using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;



namespace PregnancyGrwothTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoetusController : ControllerBase
    {
        private readonly IFoetusService _foetusService;

        public FoetusController(IFoetusService foetusService)
        {
            _foetusService = foetusService;
        }

        [HttpPost]
        [Authorize] // Yêu cầu đăng nhập
        public async Task<IActionResult> CreateFoetus([FromBody] FoetusCreateDto request)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Invalid token." });
            }

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                var foetus = await _foetusService.CreateFoetusAsync(userId, request);
                return CreatedAtAction(nameof(CreateFoetus), new { id = foetus.FoetusId }, foetus);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpGet]
        [Authorize] // Yêu cầu đăng nhập
        public async Task<IActionResult> GetFoetusesByUser()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { Message = "Invalid token." });
            }

            int userId = int.Parse(userIdClaim.Value);

            var foetuses = await _foetusService.GetFoetusesByUserIdAsync(userId);
            return Ok(foetuses);
        }

        [HttpDelete("{foetusId}")]
        [Authorize] // ✅ Chỉ cho phép người dùng đã đăng nhập
        public async Task<IActionResult> DeleteFoetus(int foetusId)
        {
            // ✅ Lấy `UserId` từ token JWT
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized(new { Message = "Unauthorized." });

            int userId = int.Parse(userIdClaim.Value);

            bool isDeleted = await _foetusService.DeleteFoetusAsync(foetusId, userId);
            return isDeleted ? Ok(new { Message = "Foetus deleted successfully." }) : NotFound(new { Message = "Foetus not found or does not belong to you." });
        }
    }
}


