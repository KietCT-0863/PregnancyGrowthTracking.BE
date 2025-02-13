using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PregnancyGrowthTracking.DAL;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly PregnancyGrowthTrackingDbContext _dbContext;
    private readonly IConfiguration _configuration;
    public AuthRepository(PregnancyGrowthTrackingDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
    {
        //  Kiểm tra Username hoặc Email đã tồn tại chưa
        var existingUser = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email || u.UserName == request.Username);

        if (existingUser != null)
        {
            return new RegisterResponseDto
            {
                Message = "Email hoặc Username đã được sử dụng.",
                Success = false
            };
        }

        //  Kiểm tra định dạng số điện thoại
        if (!Regex.IsMatch(request.Phone, @"^0\d{9}$"))
        {
            return new RegisterResponseDto
            {
                Message = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.",
                Success = false
            };
        }

        //  Kiểm tra độ mạnh của mật khẩu
        if (!Regex.IsMatch(request.Password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$"))
        {
            return new RegisterResponseDto
            {
                Message = "Mật khẩu phải chứa ít nhất một chữ cái và một số.",
                Success = false
            };
        }

        //  Tạo user mới
        var newUser = new User
        {
            UserName = request.Username,
            FullName = request.FullName,
            Email = request.Email,
            Password = request.Password, // Không hash mật khẩu
            Dob = request.Dob,
            Phone = request.Phone,
            RoleId = 2, // Mặc định là User
            Available = true
        };

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();

        return new RegisterResponseDto
        {
            Message = "Đăng ký thành công! Vui lòng đăng nhập.",
            Success = true,
            UserId = newUser.UserId
        };
    }



    //Login
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        //  Kiểm tra người dùng bằng Email hoặc Username
        var user = await _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == request.UsernameOrEmail || u.UserName == request.UsernameOrEmail);

        if (user == null || user.Password != request.Password)
        {
            throw new UnauthorizedAccessException("Invalid username/email or password.");
        }

        //  Tạo JWT Token
        string token = GenerateJwtToken(user);

        return new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role?.Role1 ?? "User"
        };
    }

    private string GenerateJwtToken(User user)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        if (key.Length < 32)
            throw new InvalidOperationException("JWT Key must be at least 32 characters long.");

        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role?.Role1 ?? "User")
    };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}
