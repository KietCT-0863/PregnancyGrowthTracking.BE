using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IAuthRepository
    {
        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
    }
}
