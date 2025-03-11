using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace PregnancyGrwothTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileImgController : ControllerBase
    {
        private readonly IS3Service _s3service;
        private readonly IUserService _userService;

        public ProfileImgController(IS3Service service, IUserService userService)
        {
            _s3service = service;
            _userService = userService;
        }

        [HttpPut("{userId}/profile-image")]
        [Authorize]
        public async Task<IActionResult> UploadProfileImage(int userId, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File không hợp lệ!");

            // ✅ 1. Upload ảnh lên S3
            var imageUrl = await _s3service.UploadFileAsync(file, userId.ToString());

            // ✅ 2. Cập nhật URL vào bảng Users
            var updated = await _userService.UpdateUserProfileImageAsync(userId, imageUrl);
            if (!updated) return NotFound("Không tìm thấy người dùng");

            return Ok(new { Message = "Cập nhật ảnh đại diện thành công", ImageUrl = imageUrl });
        }

        [HttpGet("{userId}/files")]
        [Authorize]
        public async Task<IActionResult> ListFiles(string userId)
        {
            var files = await _s3service.ListFilesAsync($"{userId}/");
            if (files == null || files.Count == 0) return NotFound("Không tìm thấy file nào.");
            return Ok(files);
        }

        [HttpGet("{userId}/profile-image")]
        [Authorize]
        public async Task<IActionResult> GetUserProfileImage(int userId)
        {
            // ✅ Lấy ảnh từ UserService
            var imageUrl = await _userService.GetUserProfileImageAsync(userId);

            if (string.IsNullOrEmpty(imageUrl))
                return NotFound("Người dùng chưa có ảnh đại diện.");

            return Ok(new { ImageUrl = imageUrl });
        }

        [HttpDelete("{userId}/profile-image")]
        [Authorize(Roles = "admin")]

        public async Task<IActionResult> DeleteProfileImage(int userId)
        {
            // ✅ Xoá URL trong DB
            var deleted = await _userService.DeleteUserProfileImageAsync(userId);
            if (!deleted) return NotFound("Không tìm thấy người dùng hoặc không có ảnh.");

            return Ok(new { Message = "Đã xoá ảnh đại diện khỏi hồ sơ." });
        }

    }
}
