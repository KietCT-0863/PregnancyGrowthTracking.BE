using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public UserController(IUserService userService, PregnancyGrowthTrackingDbContext context)
        {
            _userService = userService;
            _dbContext = context;
        }

        //  Get all users (Chỉ Admin mới có quyền truy cập)
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        //  Get user by ID
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        //  Create user
        [HttpPost]
        [Authorize(Roles = "admin")] 
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var newUser = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserId }, newUser);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        //  Update user
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto request)
        {
            try
            {
                var updated = await _userService.UpdateUserAsync(id, request);
                return updated ? Ok(new { Message = "Cập nhật người dùng thành công." }) : NotFound("Không tìm thấy người dùng.");
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Lỗi máy chủ nội bộ.", Error = ex.Message });
            }
        }



        //  Delete user
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Chỉ Admin mới có quyền xóa user
        public async Task<IActionResult> DeleteUser(int id)
        {
            var deleted = await _userService.DeleteUserAsync(id);
            return deleted ? Ok(new { Message = "User has been deactivated." }) : NotFound(new { Message = "User not found." });
        }


        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> SearchUsers([FromQuery] string fullNameOrKeyword)
        {
            //  Kiểm tra dữ liệu đầu vào có rỗng hoặc null không
            if (string.IsNullOrWhiteSpace(fullNameOrKeyword))
                return BadRequest(new { Message = "Please enter search keywords." });

            //  Kiểm tra nếu từ khóa có chứa số hoặc ký tự đặc biệt
            if (!Regex.IsMatch(fullNameOrKeyword, @"^[a-zA-Z\s]+$"))
                return BadRequest(new { Message = "Search for keywords that do not contain numbers or special characters." });

            var users = await _userService.SearchUsersByKeywordAsync(fullNameOrKeyword);

            //  Kiểm tra nếu không tìm thấy người dùng nào
            if (users == null || !users.Any())
                return NotFound(new { Message = "User not found." });

            return Ok(users);
        }

        //Đếm tổng số user có trong hệ thống trừ admin
        [HttpGet("count-total-users")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CountUsersByRole()
        {
            try
            {
                var countRole2 = await _dbContext.Users.CountAsync(u => u.RoleId == 2);
                var countRole3 = await _dbContext.Users.CountAsync(u => u.RoleId == 3);

                var totalUsers = countRole2 + countRole3;

                return Ok(new
                {
                    TotalUsers = totalUsers
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while counting users: {ex.Message}");
            }
        }

        [HttpGet("monthly-user-count")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMonthlyUserCount()
        {
            try
            {
                var users = await _dbContext.Users
                    .Where(u => u.RoleId == 2 || u.RoleId == 3)
                    .ToListAsync();

                if (!users.Any())
                    return Ok(new List<object>());

                var minDate = users.Min(u => u.CreatedAt);
                var maxDate = users.Max(u => u.CreatedAt);

                var allMonths = Enumerable.Range(0, (maxDate.Year - minDate.Year) * 12 + maxDate.Month - minDate.Month + 1)
                    .Select(offset => new DateTime(minDate.Year, minDate.Month, 1).AddMonths(offset))
                    .Select(date => new { Year = date.Year, Month = date.Month })
                    .ToList();

                var monthlyUserCount = allMonths
                    .GroupJoin(
                        users.GroupBy(u => new { u.CreatedAt.Year, u.CreatedAt.Month }),
                        month => month,
                        userGroup => userGroup.Key,
                        (month, userGroup) => new
                        {
                            Year = month.Year,
                            Month = month.Month,
                            MonthlyUsers = userGroup.SelectMany(g => g).Count()
                        })
                    .OrderBy(r => r.Year)
                    .ThenBy(r => r.Month)
                    .ToList();

                return Ok(monthlyUserCount);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while calculating monthly user count: {ex.Message}");
            }
        }

        //Update Profile By User
        [HttpPut("me")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserSelfUpdateDto request)
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int requestUserId))
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            try
            {
                var updated = await _userService.UpdateUserProfileAsync(requestUserId, request);
                return updated ? Ok(new { Message = "Profile updated successfully." }) : NotFound(new { Message = "User not found." });
            }
            catch (ValidationException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Internal server error.", Error = ex.Message });
            }
        }

        //Get profile user
        [HttpGet("me")]
        [Authorize] // Chỉ user đăng nhập mới có thể xem thông tin cá nhân
        public async Task<IActionResult> GetUserProfile()
        {
            var userIdClaim = User.FindFirst("UserId")?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int requestUserId))
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var userProfile = await _userService.GetUserProfileAsync(requestUserId);
            if (userProfile == null)
                return NotFound(new { Message = "Không tìm thấy thông tin người dùng." });

            return Ok(userProfile);
        }


    }
}

