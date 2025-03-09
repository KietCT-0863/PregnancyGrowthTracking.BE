using Microsoft.AspNetCore.Mvc;
using PregnancyGrowthTracking.BLL.Services.Vnpay;
using PregnancyGrowthTracking.DAL.DTOs.Vnpay;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http.Json;

namespace PregnancyGrwothTracking.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IVnPayService _vnPayService;
        private readonly PregnancyGrowthTrackingDbContext _dbContext;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _configuration;

        public PaymentController(IVnPayService vnPayService,
            PregnancyGrowthTrackingDbContext context,
            ILogger<PaymentController> logger,
            IConfiguration configuration)
        {
            _vnPayService = vnPayService;
            _dbContext = context;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePaymentUrlVnpay([FromBody] PaymentInformationModel model)
        {
            try
            {
                var membership = await _dbContext.Memberships
                    .FirstOrDefaultAsync(m => m.MembershipId == model.MembershipId);

                if (membership == null)
                    return NotFound("Membership not found");

                // Kiểm tra user tồn tại
                var user = await _dbContext.Users.FindAsync(model.UserId);
                if (user == null)
                    return NotFound("User not found");

                if (user.RoleId == 1 || user.RoleId == 2)
                {
                    return BadRequest(new { message = "Bạn đã là thành viên, không cần thanh toán nữa." });
                }

                _logger.LogInformation($"Creating payment for User {model.UserId}, Membership {model.MembershipId}");

                model.Amount = (decimal)(membership.Price ?? 0);
                // ép kiểu về decimal, vì membership.Price là một nullable nên phải thêm toán ?? để khi giá trị là null sẽ trả về 0

                model.OrderDescription = $"Payment for Membership ID: {model.MembershipId}|UserId: {model.UserId}";  // Thêm UserId vào OrderDescription

                var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
                return Ok(new { paymentUrl = url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("payment-callback")]
        public async Task<IActionResult> PaymentCallbackVnpay([FromQuery] string vnp_ResponseCode,
    [FromQuery] string vnp_TxnRef,
    [FromQuery] string vnp_TransactionNo,
    [FromQuery] string vnp_OrderInfo)
        {
            try
            {
                var response = _vnPayService.PaymentExecute(Request.Query);
                _logger.LogInformation($"VNPay Response: {JsonSerializer.Serialize(response)}");

                if (response.Success)
                {
                    _logger.LogInformation($"Processing OrderInfo: {response.OrderDescription}");

                    // Parse từ OrderInfo thay vì TxnRef
                    string[] parts = response.OrderDescription.Split('|');
                    if (parts.Length != 2)
                    {
                        return BadRequest(new { Success = false, Message = "Invalid order info format" });
                    }

                    if (!int.TryParse(parts[0], out int membershipId))
                    {
                        return BadRequest(new { Success = false, Message = "Invalid membership ID format" });
                    }

                    if (!int.TryParse(parts[1], out int userId))
                    {
                        return BadRequest(new { Success = false, Message = "Invalid user ID format" });
                    }

                    _logger.LogInformation($"Parsed MembershipId: {membershipId}, UserId: {userId}");

                    var membership = await _dbContext.Memberships.FindAsync(membershipId);
                    if (membership == null)
                    {
                        return BadRequest(new { Success = false, Message = "Membership not found" });
                    }

                    // Chuyển đổi USD sang VND
                    const decimal USD_TO_VND_RATE = 24500m;
                    var vndAmount = (decimal)(membership.Price ?? 0) * USD_TO_VND_RATE; 
                    // ép kiểu về decimal rồi mới nhân
                    vndAmount = Math.Round(vndAmount, 0);

                    var payment = new Payment
                    {
                        UserId = userId,
                        MembershipId = membershipId,
                        Date = DateOnly.FromDateTime(DateTime.UtcNow), // ép kiểu về DateOnly
                        TotalPrice = (double)vndAmount // ép kiểu về double
                    };


                    _dbContext.Payments.Add(payment);

                    var user = await _dbContext.Users.FindAsync(userId);
                    if (user != null && user.RoleId == 3)
                    {
                        user.RoleId = 2;
                        _dbContext.Users.Update(user);
                    }

                    await _dbContext.SaveChangesAsync();

                    return Ok(new
                    {
                        Success = true,
                        Message = "Payment processed successfully",
                        PaymentId = payment.PaymentId,
                        TransactionId = vnp_TxnRef,
                        TransactionNo = vnp_TransactionNo,
                        AmountUSD = membership.Price,
                        AmountVND = vndAmount,
                        MembershipId = membershipId,
                        UserId = userId
                    });
                }

                return BadRequest(new
                {
                    Success = false,
                    Message = "Payment failed",
                    ResponseCode = vnp_ResponseCode,
                    TransactionNo = vnp_TransactionNo
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in callback: {ex.Message}, TxnRef: {vnp_TxnRef}");
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message,
                    ResponseCode = vnp_ResponseCode,
                    TransactionNo = vnp_TransactionNo
                });
            }
        }

        [HttpGet("check-payment/{userId}")]
        public async Task<IActionResult> CheckPaymentStatus(int userId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound(new { Success = false, Message = "Người dùng không tồn tại." });
                }

                
                bool isPaymentSuccessful = user.RoleId == 2;
                return Ok(new { Success = isPaymentSuccessful });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet("user-payments/{userId}")]
        public async Task<IActionResult> GetUserPayments(int userId)
        {
            try
            {
                var payments = await _dbContext.Payments
                    .Where(p => p.UserId == userId)
                    .Include(p => p.Membership)
                    .OrderByDescending(p => p.Date)
                    .Select(p => new
                    {
                        p.PaymentId,
                        p.Date,
                        p.TotalPrice,
                        Membership = new
                        {
                            p.Membership.MembershipId,
                            p.Membership.Description,
                            p.Membership.Price
                        } 
                    })
                    .ToListAsync();

                if (!payments.Any())
                    return NotFound($"No payments found for user {userId}");

                return Ok(payments);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("payment/{paymentId}/user/{userId}")]
        public async Task<IActionResult> GetPayment(int paymentId, int userId)
        {
            try
            {
                var payment = await _dbContext.Payments
                    .Include(p => p.Membership)
                    .FirstOrDefaultAsync(p => p.PaymentId == paymentId && p.UserId == userId);

                if (payment == null)
                    return NotFound($"Payment {paymentId} not found for user {userId}");

                return Ok(new
                {
                    payment.PaymentId,
                    payment.Date,
                    payment.TotalPrice,
                    Membership = new
                    {
                        payment.Membership.MembershipId,
                        payment.Membership.Description,
                        payment.Membership.Price
                    }
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
        [HttpGet("total-payment")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetTotalPaymentAmount()
        {
            try
            {
                var totalAmount = await _dbContext.Payments.SumAsync(p => p.TotalPrice);
                return Ok(new { TotalPaymentAmount = totalAmount });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while calculating the total payment amount: {ex.Message}");
            }
        }

        [HttpGet("monthly-revenue-list")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMonthlyRevenueList()
        {
            try
            {
                // Nhóm các thanh toán theo tháng và năm, sau đó tính tổng TotalPrice
                var monthlyRevenueList = await _dbContext.Payments
                    .Where(p => p.Date.HasValue) // Lọc các bản ghi có PaymentDate không null
                    .GroupBy(p => new { p.Date.Value.Year, p.Date.Value.Month }) // Sử dụng Value để truy cập Year và Month
                    .Select(g => new
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        TotalRevenue = g.Sum(p => p.TotalPrice)
                    })
                    .OrderBy(r => r.Year)
                    .ThenBy(r => r.Month)
                    .ToListAsync();

                return Ok(monthlyRevenueList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while calculating monthly revenue list: {ex.Message}");
            }
        }
    }
}
