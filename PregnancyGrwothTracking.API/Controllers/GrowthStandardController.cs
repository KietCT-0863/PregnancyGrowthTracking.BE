using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrwothTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GrowthStandardController : Controller
    {
        private readonly IGrowthStandardServices _growthStandardService;

        public GrowthStandardController(IGrowthStandardServices growthStandardService)
        {
            _growthStandardService = growthStandardService;
        }

        [HttpGet]
        [Authorize(Roles = "admin,vip")]
        public async Task<IActionResult> GetAllGrowthStandards()
        {
            List<GrowthStandard> result = await _growthStandardService.GetGrowthStandardsAsync();
            if (result == null)
            {
                return NotFound("No growth standards found.");
            }
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddGrowthStandard([FromBody] GrowthStandardDTO growthStandard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _growthStandardService.AddGrowthStandardAsync(growthStandard);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                // dùng để bắt Exception GestationalAge đã tồn tại
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateGrowthStandard([FromBody] GrowthStandardUpdateDTO growthStandardDto)
        {
            if (growthStandardDto == null)
            {
                return BadRequest("Invalid request data.");
            }

            try
            {
                await _growthStandardService.UpdateGrowthStandardAsync(growthStandardDto);
                return Ok(new { message = "Growth Standard updated successfully!" });
            }
            catch (ArgumentException ex)
            {
                // dùng để bắt Exception GrowthStandard không tồn tại
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
