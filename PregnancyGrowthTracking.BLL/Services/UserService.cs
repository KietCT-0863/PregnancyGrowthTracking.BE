using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //  Lấy tất cả User
        public async Task<IEnumerable<UserResponseDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email
            }).ToList();
        }

        //  Lấy User theo ID
        public async Task<UserResponseDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            return user == null ? null : new UserResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email
            };
        }

        //  Tạo User mới
        public async Task<UserResponseDto> CreateUserAsync(UserCreateDto request)
        {
            // Kiểm tra Username đã tồn tại chưa
            if (await _userRepository.UserNameExistsAsync(request.UserName))
                throw new Exception("Username already exists");

            var user = new User
            {
                UserName = request.UserName,
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password, // Lưu ý: Không hash mật khẩu ở đây
                Dob = request.Dob,
                Phone = request.Phone,
                Available = request.Available,
                RoleId = request.RoleId
            };

            var createdUser = await _userRepository.CreateUserAsync(user);

            return new UserResponseDto
            {
                UserId = createdUser.UserId,
                UserName = createdUser.UserName,
                FullName = createdUser.FullName,
                Email = createdUser.Email
            };
        }

        //  Cập nhật User
        public async Task<bool> UpdateUserAsync(int id, UserUpdateDto request)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return false;

            // Chỉ cập nhật các trường không null
            if (!string.IsNullOrEmpty(request.UserName)) user.UserName = request.UserName;
            if (!string.IsNullOrEmpty(request.FullName)) user.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Email)) user.Email = request.Email;
            if (!string.IsNullOrEmpty(request.Password)) user.Password = request.Password;
            if (request.Dob.HasValue) user.Dob = request.Dob;
            if (!string.IsNullOrEmpty(request.Phone)) user.Phone = request.Phone;
            if (request.Available.HasValue) user.Available = request.Available;
            if (request.RoleId.HasValue) user.RoleId = request.RoleId;

            return await _userRepository.UpdateUserAsync(user);
        }

        //  Xóa User
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }
    }
}
