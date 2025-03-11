using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.API.Controllers
{
    [ApiController]
    [Route("api/foetus/{foetusId}/growth-data")]
    public class GrowthDataController : ControllerBase
    {
        private readonly IGrowthDataService _growthDataService;

        public GrowthDataController(IGrowthDataService growthDataService)
        {
            _growthDataService = growthDataService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddOrUpdateGrowthData(int foetusId, [FromBody] GrowthDataDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized(new { Message = "Unauthorized." });

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                bool isSaved = await _growthDataService.IsAddOrUpdate(foetusId, userId, request);
                if (!isSaved)
                {
                    return StatusCode(500, new { Message = "Failed to save data." });
                }

                // Kiểm tra các chỉ số và trả về cảnh báo
                var alerts = await _growthDataService.AlertReturnWithRange(request);
                
                // Tạo response object
                var response = new
                {
                    Message = "Growth data saved successfully.",
                    Alerts = alerts,
                    //HasWarnings = alerts.Any(x => x.Value.IsAlert)
                };

                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal Server Error.", Error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize] // ✅ Chỉ cho phép người dùng đã đăng nhập
        public async Task<IActionResult> GetGrowthDataByFoetusId(int foetusId)
        {
            // ✅ Lấy `UserId` từ token JWT
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null) return Unauthorized(new { Message = "Unauthorized." });

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                var growthData = await _growthDataService.GetGrowthDataByFoetusIdAsync(foetusId, userId);
                return Ok(growthData);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        //[HttpPut("update")]
        //[Authorize]
        //public async Task<IActionResult> UpdateGrowthData([FromBody] GrowthDataDto request)
        //{
        //    var userIdClaim = User.FindFirst("UserId");
        //    if (userIdClaim == null) return Unauthorized(new { Message = "Unauthorized." });

        //    int userId = int.Parse(userIdClaim.Value);

        //    try
        //    {
        //        bool updated = await _growthDataService.UpdateGrowthDataAsync(userId, request);
        //        return updated ? Ok(new { Message = "Growth data updated successfully." })
        //                       : NotFound(new { Message = "Growth data not found or access denied." });
        //    }
        //    catch (KeyNotFoundException ex)
        //    {
        //        return NotFound(new { Message = ex.Message });
        //    }
        //}
    }
}
