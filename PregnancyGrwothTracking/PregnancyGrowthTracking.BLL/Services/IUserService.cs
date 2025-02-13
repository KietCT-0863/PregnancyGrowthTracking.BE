using PregnancyGrowthTracking.DAL.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDto>> GetAllUsersAsync();
        Task<UserResponseDto?> GetUserByIdAsync(int id);
        Task<UserResponseDto> CreateUserAsync(UserCreateDto request);
        Task<bool> UpdateUserAsync(int id, UserUpdateDto request);
        Task<bool> DeleteUserAsync(int id);
    }
}
