using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
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
                Role = u.Role?.Role1               
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
                Role = user.Role?.Role1
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
                Email = createdUser.Email
            };
        }

        //  Cập nhật User
        public async Task<bool> UpdateUserAsync(int userId, UserUpdateDto request)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new KeyNotFoundException("Không tìm thấy người dùng.");
            //  Kiểm tra UserName (phải có ít nhất 4 ký tự, không chứa ký tự đặc biệt ngoài "_" và ".")
            if (!string.IsNullOrWhiteSpace(request.UserName) &&
                !Regex.IsMatch(request.UserName, @"^(?![_\.])[a-zA-Z0-9._]{4,30}(?<![_\.])$"))
            {
                throw new ArgumentException("Username phải có ít nhất 4 ký tự, không chứa ký tự đặc biệt ngoại trừ _ và không được bắt đầu hoặc kết thúc bằng _ hoặc .");
            }

            //  Kiểm tra FullName (không chứa số & ký tự đặc biệt)
            if (!string.IsNullOrWhiteSpace(request.FullName) && !Regex.IsMatch(request.FullName, @"^[a-zA-Z\s]{2,50}$"))
                throw new ArgumentException("Họ và tên phải chứa từ 2-50 ký tự, không chứa số hoặc ký tự đặc biệt.");

            //  Kiểm tra Email (nếu có cập nhật)
            if (!string.IsNullOrWhiteSpace(request.Email) && !Regex.IsMatch(request.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new ArgumentException("Email không hợp lệ.");

            //  Kiểm tra Password (nếu có cập nhật)
            if (!string.IsNullOrWhiteSpace(request.Password) && !Regex.IsMatch(request.Password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"))
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự, chứa cả chữ cái và số.");

            //  Kiểm tra Ngày sinh (nếu có cập nhật)
            if (request.Dob.HasValue && !Regex.IsMatch(request.Dob.Value.ToString("yyyy/MM/dd"), @"^\d{4}/\d{2}/\d{2}$"))
                throw new ArgumentException("Ngày sinh phải theo định dạng YYYY/MM/DD.");

            // Kiểm tra số điện thoại (nếu có cập nhật)
            if (!string.IsNullOrWhiteSpace(request.Phone) && !Regex.IsMatch(request.Phone, @"^0\d{9}$"))
                throw new ArgumentException("Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.");

            //  Kiểm tra RoleId (nếu có cập nhật)
            if (request.RoleId.HasValue && (request.RoleId < 1 || request.RoleId > 3))
                throw new ArgumentException("RoleId phải nằm trong khoảng 1-3.");

            //  Cập nhật dữ liệu
            user.UserName = request.UserName ?? user.UserName;
            user.FullName = request.FullName ?? user.FullName;
            user.Email = request.Email ?? user.Email;
            user.Password = request.Password ?? user.Password;
            user.Dob = request.Dob ?? user.Dob;
            user.Phone = request.Phone ?? user.Phone;
            user.Available = request.Available ?? user.Available;
            user.RoleId = request.RoleId ?? user.RoleId;

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
                Role = u.Role?.Role1  
            }).ToList();
        }

    }
}
