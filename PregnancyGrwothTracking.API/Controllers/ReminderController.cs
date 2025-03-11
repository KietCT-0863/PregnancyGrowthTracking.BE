using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace PregnancyGrwothTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // ✅ Chỉ user đăng nhập mới có quyền gọi API
    public class ReminderController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "UserId not found in token" });
            }

            int userId = int.Parse(userIdClaim.Value);

            var reminder = await _reminderService.CreateReminderAsync(userId, request);

            return Ok(new { message = "Reminder created successfully!", reminderId = reminder.RemindId });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetReminderHistory()
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "UserId not found in token" });
            }

            int userId = int.Parse(userIdClaim.Value);

            var history = await _reminderService.GetReminderHistoryAsync(userId);

            return Ok(history);
        }
        [HttpDelete("delete/{remindId}")]
        public async Task<IActionResult> DeleteReminder(int remindId)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "UserId not found in token" });
            }

            int userId = int.Parse(userIdClaim.Value);

            bool isDeleted = await _reminderService.DeleteReminderAsync(userId, remindId);

            if (!isDeleted)
            {
                return NotFound(new { message = "Reminder not found or you don't have permission to delete it." });
            }

            return Ok(new { message = "Reminder deleted successfully!" });
        }

        [HttpPut("update/{remindId}")]
        public async Task<IActionResult> UpdateReminder(int remindId, [FromBody] UpdateReminderDto request)
        {
            var userIdClaim = User.FindFirst("UserId");
            if (userIdClaim == null)
            {
                return Unauthorized(new { message = "UserId not found in token" });
            }

            int userId = int.Parse(userIdClaim.Value);



            bool isUpdated = await _reminderService.UpdateReminderAsync(userId, remindId, request);

            if (!isUpdated)
            {
                return NotFound(new { message = "Reminder not found or you don't have permission to update it." });
            }

            return Ok(new { message = "Reminder updated successfully!" });
        }

    }
}
