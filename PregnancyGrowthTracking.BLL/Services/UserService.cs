using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
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
                Email = u.Email,
                Phone = u.Phone,
                ProfileImageUrl = u.ProfileImageUrl,
                Dob = u.Dob,
                Available = u.Available ?? false,
                RoleId = u.RoleId ?? 0,
                Role = u.Role?.RoleName
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
                Email = user.Email,
                Phone = user.Phone,
                ProfileImageUrl = user.ProfileImageUrl,
                Dob = user.Dob,
                Available = user.Available ?? false,
                RoleId = user.RoleId ?? 0,
                Role = user.Role?.RoleName
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
                Password = request.Password, 
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
                Email = createdUser.Email,
                Phone = createdUser.Phone,
                Dob= createdUser.Dob,
                Available = createdUser.Available ?? false,
                RoleId = user.RoleId ?? 0,
                Role = user.Role?.RoleName
            };
        }

        //  Cập nhật User
        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDto request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("Không tìm thấy người dùng.");

            var errors = new List<string>();

            //  Kiểm tra chỉ khi admin nhập giá trị mới
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                if (request.UserName.Length < 4 || request.UserName.Length > 30)
                    errors.Add("Username must be between 4 and 30 characters.");
                if (!Regex.IsMatch(request.UserName, @"^(?![0-9]+$)(?!.*\s)(?!.*[_\.]$)(?!^[_\.])(?!.*[_\.]{2,})[a-zA-Z0-9._]+$"))
                    errors.Add("Username can only contain letters, numbers, underscores (_), or dots (.) but not at the beginning or end.");
            }

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                if (request.FullName.Length < 4 || request.FullName.Length > 20)
                    errors.Add("Full Name must be between 4 and 20 characters.");
                if (!Regex.IsMatch(request.FullName, @"^[\p{L}\s]+$"))
                    errors.Add("Full Name can only contain letters and spaces.");
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (!new EmailAddressAttribute().IsValid(request.Email))
                    errors.Add("Invalid email format.");
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password.Length < 6 || request.Password.Length > 30)
                    errors.Add("Password must be between 6 and 30 characters.");
                if (!Regex.IsMatch(request.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$"))
                    errors.Add("Password must contain at least one lowercase letter, one uppercase letter, and one number.");
            }

            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                if (!Regex.IsMatch(request.Phone, @"^(0\d{9}|\+84\d{9})$"))
                    errors.Add("Phone number must be 10 digits and start with 0 or +84.");
            }

            if (request.RoleId.HasValue)
            {
                var validRoles = new List<int> { 1, 2, 3 }; // Chỉ cho phép RoleId 1, 2, 3
                if (!validRoles.Contains(request.RoleId.Value))
                {
                    errors.Add("RoleId must be between 1 and 3.");
                }
            }

            // Nếu có lỗi validation, trả về lỗi 400
            if (errors.Count > 0)
                throw new ValidationException(string.Join("; ", errors));

            //  Nếu admin không nhập, giữ nguyên dữ liệu cũ
            user.UserName = !string.IsNullOrWhiteSpace(request.UserName) ? request.UserName : user.UserName;
            user.FullName = !string.IsNullOrWhiteSpace(request.FullName) ? request.FullName : user.FullName;
            user.Email = !string.IsNullOrWhiteSpace(request.Email) ? request.Email : user.Email;
            user.Password = !string.IsNullOrWhiteSpace(request.Password) ? request.Password : user.Password;
            user.Dob = request.Dob ?? user.Dob;
            user.Phone = !string.IsNullOrWhiteSpace(request.Phone) ? request.Phone : user.Phone;
            user.Available = request.Available ?? user.Available;
            user.RoleId = request.RoleId ?? user.RoleId; //  Đảm bảo chỉ cập nhật RoleId nếu hợp lệ

            return await _userRepository.UpdateUserAsync(user);
        }




        //  Xóa User
        public async Task<bool> DeleteUserAsync(int id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }


        public async Task<bool> UpdateUserProfileImageAsync(int userId, string imageUrl)
        {
            return await _userRepository.UpdateUserProfileImageAsync(userId, imageUrl);
        }

        public async Task<string?> GetUserProfileImageAsync(int userId)
        {
            //  Gọi repository để lấy URL ảnh
            return await _userRepository.GetUserProfileImageAsync(userId);
        }

        public async Task<bool> DeleteUserProfileImageAsync(int userId)
        {
            //  Gọi tới repository
            return await _userRepository.DeleteUserProfileImageAsync(userId);
        }

        public async Task<IEnumerable<UserResponseDto>> SearchUsersByKeywordAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword)) return new List<UserResponseDto>();

            // Loại bỏ số và ký tự đặc biệt, chỉ giữ lại chữ cái và khoảng trắng
            keyword = Regex.Replace(keyword, "[^a-zA-Z\\s]", "").Trim();

            var users = await _userRepository.SearchUsersByKeywordAsync(keyword);

            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                UserName = u.UserName,
                FullName = u.FullName,
                Email = u.Email,
                Phone = u.Phone,
                ProfileImageUrl = u.ProfileImageUrl,
                Dob = u.Dob,
                Available = u.Available ?? false,  
                RoleId = u.RoleId ?? 0,
                Role = u.Role?.RoleName
            }).ToList();
        }

        //Update profile by User
        public async Task<bool> UpdateUserProfileAsync(int userId, UserSelfUpdateDto request)
        {
            var user = await _userRepository.GetUserProfileAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("User not found.");

            var errors = new List<string>();

            // Kiểm tra chỉ khi user nhập giá trị mới
            if (!string.IsNullOrWhiteSpace(request.UserName))
            {
                if (request.UserName.Length < 4 || request.UserName.Length > 30)
                    errors.Add("Username must be between 4 and 30 characters.");
                if (!Regex.IsMatch(request.UserName, @"^(?![0-9]+$)(?!.*\s)(?!.*[_\.]$)(?!^[_\.])(?!.*[_\.]{2,})[a-zA-Z0-9._]+$"))
                    errors.Add("Username can only contain letters, numbers, underscores (_), or dots (.) but not at the beginning or end.");
            }

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                if (request.FullName.Length < 4 || request.FullName.Length > 20)
                    errors.Add("Full Name must be between 4 and 20 characters.");
                if (!Regex.IsMatch(request.FullName, @"^[\p{L}\s]+$"))
                    errors.Add("Full Name can only contain letters and spaces.");
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                if (!new EmailAddressAttribute().IsValid(request.Email))
                    errors.Add("Invalid email format.");
            }

            if (!string.IsNullOrWhiteSpace(request.Password))
            {
                if (request.Password.Length < 6 || request.Password.Length > 30)
                    errors.Add("Password must be between 6 and 30 characters.");
                if (!Regex.IsMatch(request.Password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$"))
                    errors.Add("Password must contain at least one lowercase letter, one uppercase letter, and one number.");
            }

            if (!string.IsNullOrWhiteSpace(request.Phone))
            {
                if (!Regex.IsMatch(request.Phone, @"^0[0-9]{9}$"))
                    errors.Add("Phone number must be 10 digits and start with 0.");
            }

            // Nếu có lỗi validation, trả về lỗi 400
            if (errors.Count > 0)
                throw new ValidationException(string.Join("; ", errors));

            // Nếu user không nhập, giữ nguyên dữ liệu cũ
            user.UserName = !string.IsNullOrWhiteSpace(request.UserName) ? request.UserName : user.UserName;
            user.FullName = !string.IsNullOrWhiteSpace(request.FullName) ? request.FullName : user.FullName;
            user.Email = !string.IsNullOrWhiteSpace(request.Email) ? request.Email : user.Email;
            user.Password = !string.IsNullOrWhiteSpace(request.Password) ? request.Password : user.Password;
            user.Dob = request.Dob ?? user.Dob;
            user.Phone = !string.IsNullOrWhiteSpace(request.Phone) ? request.Phone : user.Phone;

            return await _userRepository.UpdateUserAsync(user);
        }

        public async Task<UserProfileResponseDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserProfileAsync(userId);
            if (user == null)
                return null;

            return new UserProfileResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                FullName = user.FullName,
                Email = user.Email,
                Dob = user.Dob,
                Phone = user.Phone,
                ProfileImageUrl = user.ProfileImageUrl
            };
        }

    }
}
