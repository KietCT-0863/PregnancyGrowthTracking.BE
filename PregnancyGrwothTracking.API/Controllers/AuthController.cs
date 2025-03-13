using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.DTOs;
using System.Threading.Tasks;

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

        //login with google
        [HttpPost("signin-google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestDto request)
        {
            try
            {
                var response = await _authService.LoginWithGoogleAsync(request.IdToken);
                return Ok(response);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
    }
}
