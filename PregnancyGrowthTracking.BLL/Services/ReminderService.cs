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
            font-family: 'Arial', sans-serif;
            background-color: #f9f9f9;
            padding: 20px;
            margin: 0;
        }}
        .email-container {{
            background-color: #ffffff;
            max-width: 600px;
            margin: auto;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
            text-align: center;
            border: 1px solid #e0e0e0;
        }}
        .header {{
            font-size: 24px;
            font-weight: bold;
            color: #2E7D32;
            padding: 15px;
            background-color: #E8F5E9;
            border-radius: 10px 10px 0 0;
        }}
        .content {{
            font-size: 16px;
            color: #444;
            line-height: 1.6;
            text-align: left;
            padding: 15px;
        }}
        .info-box {{
            background-color: #f0f8ff;
            padding: 15px;
            border-radius: 8px;
            margin-top: 15px;
            text-align: left;
            border-left: 5px solid #2196F3;
        }}
        .button {{
            display: inline-block;
            background-color: #42A5F5;
            color: white;
            padding: 12px 20px;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            font-weight: bold;
            margin-top: 20px;
            transition: background 0.3s ease;
        }}
        .button:hover {{
            background-color: #1E88E5;
        }}
        .footer {{
            font-size: 14px;
            color: #777;
            padding-top: 20px;
            border-top: 1px solid #ddd;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>✅ Nhắc Nhở Đã Được Tạo Thành Công!</div>
        <div class='content'>
            <p>Xin chào <strong>{user.FullName}</strong>,</p>
            <p>Bạn vừa tạo một lời nhắc thành công trên hệ thống <strong>Pregnancy Growth Tracking</strong>.</p>
            <div class='info-box'>
                <p><strong>📌 Tiêu đề:</strong> {createdReminder.Title}</p>
                <p><strong>📖 Nội dung:</strong> {createdReminder.Notification}</p>
                <p><strong>🗂️ Loại nhắc nhở:</strong> {createdReminder.ReminderType}</p>
                <p><strong>🗓️ Ngày:</strong> {createdReminder.Date:dd/MM/yyyy}</p>
                <p><strong>⏰ Giờ:</strong> {createdReminder.Time}</p>
            </div>
           <a class='button' href='http://localhost:5173/member/calendar-detail/{createdReminder.RemindId}'>📅 Xem chi tiết</a>
        </div>
        <p class='footer'>🤰<strong>Pregnancy Growth Tracking</strong> luôn đồng hành cùng bạn trong hành trình làm mẹ!</p>
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
