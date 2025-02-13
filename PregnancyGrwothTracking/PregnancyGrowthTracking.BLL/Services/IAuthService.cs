using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);

        Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request);
    }
}
