using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
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
    private readonly HttpClient _httpClient;
    public AuthRepository(PregnancyGrowthTrackingDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _httpClient = new HttpClient();
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
        if (request.Dob != default && !Regex.IsMatch(request.Dob.ToString("yyyy/MM/dd"), @"^\d{4}/\d{2}/\d{2}$"))
        {
            return new RegisterResponseDto
            {
                Message = "Ngày sinh không đúng định dạng YYYY/MM/DD.",
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
            Password = request.Password, 
            Dob = request.Dob,
            Phone = request.Phone,
            RoleId = 3, // Mặc định là Guest
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
            Role = user.Role?.RoleName ?? "User",
            UserId = user.UserId,
            ProfileImageUrl = user.ProfileImageUrl

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
        new Claim(ClaimTypes.Role, user.Role?.RoleName ?? "User")
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

    //Login with google
    public async Task<LoginResponseDto> LoginWithGoogleAsync(string idToken)
    {
        var googleClientId = _configuration["Authentication:Google:ClientId"];
        var googleTokenValidationUrl = $"https://oauth2.googleapis.com/tokeninfo?id_token={idToken}";

        // Gửi yêu cầu đến Google để xác minh ID Token
        var response = await _httpClient.GetAsync(googleTokenValidationUrl);
        if (!response.IsSuccessStatusCode)
        {
            throw new UnauthorizedAccessException("Invalid Google ID Token.");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Google Response: " + jsonResponse); // Debug

        using var jsonDoc = JsonDocument.Parse(jsonResponse);
        var root = jsonDoc.RootElement;

        var email = root.TryGetProperty("email", out var emailProp) ? emailProp.GetString() : null;
        var aud = root.TryGetProperty("aud", out var audProp) ? audProp.GetString() : null;

        Console.WriteLine($"Received AUD: {aud}"); // Debug kiểm tra Client ID từ Google

        if (string.IsNullOrEmpty(email))
        {
            throw new UnauthorizedAccessException("Google login failed: Missing email.");
        }

        if (aud != googleClientId)
        {
            throw new UnauthorizedAccessException($"Invalid Google Client ID. Received: {aud}, Expected: {googleClientId}");
        }

        //  Gọi API để lấy ảnh đại diện từ Google
        var googleUserInfoUrl = "https://www.googleapis.com/oauth2/v1/userinfo?alt=json&access_token=" + idToken;
        var userInfoResponse = await _httpClient.GetAsync(googleUserInfoUrl);

        string profileImageUrl = null;
        if (userInfoResponse.IsSuccessStatusCode)
        {
            var userInfoJson = await userInfoResponse.Content.ReadAsStringAsync();
            using var userInfoDoc = JsonDocument.Parse(userInfoJson);
            profileImageUrl = userInfoDoc.RootElement.TryGetProperty("picture", out var pictureProp) ? pictureProp.GetString() : null;
        }

        // Kiểm tra user trong database
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            // Nếu user chưa có trong database, tạo tài khoản mới
            user = new User
            {
                UserName = email.Split('@')[0],
                FullName = root.TryGetProperty("name", out var nameProp) ? nameProp.GetString() : "Google User",
                Email = email,
                Password = "",
                RoleId = 3,
                Available = true,
                ProfileImageUrl = profileImageUrl // Lưu ảnh vào database
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            // Cập nhật ảnh nếu chưa có hoặc khác ảnh cũ
            if (string.IsNullOrEmpty(user.ProfileImageUrl) || user.ProfileImageUrl != profileImageUrl)
            {
                user.ProfileImageUrl = profileImageUrl;
                _dbContext.Users.Update(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        string token = GenerateJwtToken(user);

        return new LoginResponseDto
        {
            Token = token,
            UserName = user.UserName,
            Email = user.Email,
            Role = "Guest",
            ProfileImageUrl = user.ProfileImageUrl,
            UserId = user.UserId
        };
    }

}
