using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IEmailService _emailService;
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public AuthService(IAuthRepository authRepository, IEmailService emailService, PregnancyGrowthTrackingDbContext dbContext)
        {
            _authRepository = authRepository;
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var result = await _authRepository.RegisterAsync(request);

            if (result.Success)
            {
                string subject = "🎉 Chào mừng bạn đến với Pregnancy Growth Tracking!";

                // Sinh JWT Token đăng nhập trực tiếp
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                string token = _authRepository.GenerateJwtToken(user);
                string loginLink = $"https://pregnancy-growth-tracking.vercel.app/login?token={token}";

                string body = $@"
<div style=""font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; background-color: #fff0f6; max-width: 600px; margin: auto; border: 1px solid #f8d6e4; border-radius: 12px; padding: 24px;"">
    
    <!-- Icon mẹ bầu -->
    <div style=""text-align: center;"">
        <img src=""https://i.postimg.cc/cL1H66xt/Logo-bau-02-2.png"" alt=""Pregnant Mom Icon"" width=""120"" style=""margin-bottom: 20px;"" />
    </div>

    <h2 style=""color: #d63384; text-align: center;"">👋 Xin chào {request.FullName ?? request.Username}!</h2>

    <p style=""font-size: 16px; color: #333;"">
    Cảm ơn bạn đã đăng ký tài khoản tại <strong>Pregnancy Growth Tracking</strong> 💖.
</p>
<p style=""font-size: 16px; color: #333;"">
    Chào mừng bạn đến với <strong>Pregnancy Growth Tracking</strong> 💖 — nơi bạn có thể theo dõi thai kỳ, nhận lời khuyên hữu ích và kết nối cùng hơn 10.000 mẹ bầu khác.
</p>

    <div style=""text-align: center; margin: 30px 0;"">
        <a href=""{loginLink}""
           style=""background-color: #ff66a6; color: white; padding: 14px 28px; font-size: 16px; text-decoration: none; border-radius: 8px;"">
           🔐 Đăng nhập ngay
        </a>
    </div>
    <hr style=""border: none; border-top: 1px solid #f3cce0; margin: 24px 0;""/>

    <p style=""color: #888; font-size: 13px;"">
        Nếu bạn không đăng ký tài khoản, vui lòng bỏ qua email này.
    </p>

    <p style=""color: #888; font-size: 13px;"">
        Trân trọng,<br/>
        <em>Đội ngũ Pregnancy Growth Tracking 🌸</em>
    </p>
</div>";

                await _emailService.SendEmailAsync(request.Email, subject, body);
            }

            return result;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            return await _authRepository.LoginAsync(request);
        }

        public async Task<LoginResponseDto> LoginWithGoogleAsync(string idToken)
        {
            return await _authRepository.LoginWithGoogleAsync(idToken);
        }
    }
}
