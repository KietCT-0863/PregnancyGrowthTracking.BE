using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace PregnancyGrowthTracking.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
            {
                return BadRequest(new { message = result.Message });
            }

            return Ok(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginAsync(request);
                
                //var cookieOptions = new CookieOptions
                //{
                //    HttpOnly = true,    // cookies chỉ đc gửi trong HTTP request
                //    Secure = true,      // sử dụng HTTPS
                //};

                //// Lưu token vào cookie

                //Response.Cookies.Append("JWTToken", response.Token, cookieOptions);

                //return Ok(new
                //{
                //    message = "Login successful",
                //    token = response,
                //});

                return Ok(response); 
            }
            catch (UnauthorizedAccessException ex)
            {
                if (ex.Message == "Cút")
                {
                    return Unauthorized(new { message = ex.Message });
                }

                return Unauthorized(new { message = "Invalid username/email or password." });
            }
        }

        //[HttpPost("logout")]
        //public IActionResult Logout()
        //{
            // Xóa cookie JWT token
        //    Response.Cookies.Delete("JWTToken", new CookieOptions
        //    {
        //        HttpOnly = true,
        //        Secure = true,
        //    });

        //    return Ok(new 
        //    { 
        //        message = "Đăng xuất thành công",
        //    });
        //}

        //login with google
        [HttpPost("signin-google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginWithGoogleAsync(request.IdToken);
                
                // Tạo cookie options
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                };

                // Lưu token vào cookie
                Response.Cookies.Append("JWTToken", response.Token, cookieOptions);

                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
