using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
namespace PregnancyGrowthTracking.BLL.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public ReminderService(IReminderRepository reminderRepository, IUserRepository userRepository, IEmailService emailService, PregnancyGrowthTrackingDbContext dbContext)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task<ReminderResponseDto> CreateReminderAsync(int userId, CreateReminderDto request)
        {
            // ✅ Kiểm tra nếu `Date` nhỏ hơn hôm nay thì báo lỗi
            if (request.Date.HasValue && request.Date.Value.Date < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Date must be today or in the future.");
            }
            // ✅ Lấy giờ hiện tại theo giờ Việt Nam
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var nowUtc = DateTime.UtcNow;
            var nowVietnam = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
            var nowTime = nowVietnam.TimeOfDay; // Chỉ lấy giờ & phút

            // ✅ Kiểm tra nếu `Time` hợp lệ
            if (!TimeSpan.TryParse(request.Time, out TimeSpan userTime))
            {
                throw new ArgumentException("Lỗi: Định dạng giờ không hợp lệ! Hãy nhập theo định dạng HH:mm.");
            }

            // 🚫 Không cho phép nhập thời gian nhỏ hơn thời gian hiện tại
            if (request.Date.HasValue && request.Date.Value.Date == nowVietnam.Date && userTime <= nowTime)
            {
                throw new ArgumentException($"Lỗi: Giờ {request.Time} không hợp lệ! Hãy nhập thời gian lớn hơn thời gian hiện tại ({nowVietnam:HH:mm}).");
            }



            var newReminder = new UserReminder
            {
                UserId = userId,
                Date = request.Date ?? DateTime.UtcNow,  // Nếu `null`, lấy ngày hiện tại
                Time = request.Time,
                Title = request.Title,
                Notification = request.Notification,
                ReminderType = request.ReminderType,
                IsEmailSent = false  // ❌ Không đánh dấu là đã gửi vì nó sẽ được kiểm tra trong `ReminderScheduler`
            };

            var createdReminder = await _reminderRepository.CreateReminderAsync(newReminder);

            // ✅ Tìm email của người dùng
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || string.IsNullOrEmpty(user.Email))
            {
                throw new ArgumentException("User email not found.");
            }

            // ✅ Gửi email ngay lập tức sau khi tạo Reminder
            string subject = $"🔔 Tạo lời nhắc thành công: {createdReminder.Title}";
            string body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
  <meta charset='UTF-8'>
  <meta name='viewport' content='width=device-width, initial-scale=1.0'>
  <title>✅ Nhắc Nhở Đã Được Tạo Thành Công!</title>
  <style>
    body {{
      font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
      background-color: #ffe6f0; /* Hồng pastel nhạt toàn trang */
      margin: 0;
      padding: 0;
    }}
    .email-container {{
      background-color: #fff0f5; /* Nền hồng nhẹ cho nội dung chính */
      max-width: 600px;
      margin: 40px auto;
      padding: 25px;
      border-radius: 12px;
      box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
      border: 1px solid #f8bbd0;
    }}
    .header {{
      font-size: 22px;
      font-weight: bold;
      color: #d81b60;
      background-color: #fce4ec;
      padding: 16px;
      border-radius: 10px 10px 0 0;
      text-align: center;
    }}
    .content {{
      font-size: 15px;
      color: #444;
      line-height: 1.6;
      padding: 20px;
      text-align: left;
    }}
    .info-box {{
      background-color: #ffe4f0;
      padding: 16px;
      border-radius: 8px;
      border-left: 5px solid #f06292;
      margin-top: 12px;
    }}
    .info-box p {{
      margin: 6px 0;
      color: #333;
    }}
    .info-box strong {{
      color: #c2185b;
    }}
    .btn {{
      display: inline-block;
      background-color: #e91e63;
      color: #fff;
      padding: 12px 20px;
      text-decoration: none;
      border-radius: 6px;
      font-weight: bold;
      margin-top: 20px;
      transition: background 0.3s ease;
    }}
    .btn:hover {{
      background-color: #c2185b;
    }}
    .footer {{
      font-size: 13px;
      color: #777;
      text-align: center;
      margin-top: 30px;
      border-top: 1px solid #f8bbd0;
      padding-top: 15px;
    }}
  </style>
</head>
<body>
  <div class='email-container'>
    <div class='header'>✅ Nhắc Nhở Đã Được Tạo Thành Công!</div>
    <div class='content'>
     <p>👋 Xin chào <strong>{user.FullName}</strong>,</p>
<p>🙏 Cảm ơn bạn đã sử dụng hệ thống 
  <span style='display: inline-block; background-color: #f8bbd0; color: #c2185b; padding: 3px 10px; border-radius: 6px; font-weight: bold;'>
    Pregnancy Growth Tracking
  </span>.
</p>
<p>⏰ Hệ thống sẽ gửi thông báo nhắc nhở đến bạn <strong>trước 1 tiếng</strong>.</p>
<p>📬 Vui lòng kiểm tra Gmail của bạn để đảm bảo nhận được thông báo đúng lúc nhé!</p>



      <div class='info-box'>
        <p><strong>📌 Tiêu đề:</strong> {createdReminder.Title}</p>
        <p><strong>📖 Nội dung:</strong> {createdReminder.Notification}</p>
        <p><strong>🗂️ Loại nhắc nhở:</strong> {createdReminder.ReminderType}</p>
        <p><strong>🗓️ Ngày:</strong> {createdReminder.Date:dd/MM/yyyy}</p>
        <p><strong>⏰ Giờ:</strong> {createdReminder.Time}</p>
      </div>

      <a class='btn' href='https://pregnancy-growth-tracking.vercel.app/member/calendar-detail/{createdReminder.RemindId}'>📅 Xem chi tiết</a>

      <p style='margin-top: 20px;'>💖 Hãy tiếp tục cập nhật những cột mốc quan trọng trong hành trình làm mẹ nhé!</p>
    </div>
    <div class='footer'>
      🤰 <strong>Pregnancy Growth Tracking</strong> luôn đồng hành cùng bạn trong hành trình làm mẹ!
    </div>
  </div>
</body>
</html>";





            bool emailSent = await _emailService.SendEmailAsync(user.Email, subject, body);

            if (emailSent)
            {
                Console.WriteLine($"📩 [EMAIL SENT] Reminder '{createdReminder.Title}' sent to {user.Email}");
            }
            else
            {
                Console.WriteLine($"❌ [EMAIL FAILED] Could not send email to {user.Email}");
            }

            return new ReminderResponseDto
            {
                RemindId = createdReminder.RemindId,
                Date = createdReminder.Date ?? DateTime.MinValue,
                Time = createdReminder.Time,
                Title = createdReminder.Title,
                Notification = createdReminder.Notification,
                ReminderType = createdReminder.ReminderType,
                IsEmailSent = createdReminder.IsEmailSent
            };
        }

        public async Task<IEnumerable<ReminderHistoryDto>> GetReminderHistoryAsync(int userId)
        {
            var reminders = await _reminderRepository.GetReminderHistoryAsync(userId);

            return reminders.Select(r => new ReminderHistoryDto
            {
                RemindId = r.RemindId,
                Date = r.Date,
                Time = r.Time,
                Title = r.Title,
                Notification = r.Notification,
                ReminderType = r.ReminderType,
                IsEmailSent = r.IsEmailSent
            }).ToList();
        }
        public async Task<bool> DeleteReminderAsync(int userId, int remindId)
        {
            return await _reminderRepository.DeleteReminderAsync(userId, remindId);
        }
        public async Task<bool> UpdateReminderAsync(int userId, int remindId, UpdateReminderDto request)
        {
            return await _reminderRepository.UpdateReminderAsync(userId, remindId, request);
        }
    }
}
