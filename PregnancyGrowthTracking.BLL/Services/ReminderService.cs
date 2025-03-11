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
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            padding: 20px;
        }}
        .email-container {{
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1);
            max-width: 600px;
            margin: auto;
            text-align: center;
        }}
        .header {{
            font-size: 22px;
            font-weight: bold;
            color: #2E7D32;
            margin-bottom: 10px;
        }}
        .content {{
            font-size: 16px;
            color: #333;
            line-height: 1.6;
            text-align: left;
        }}
        .info-box {{
            background-color: #e8f5e9;
            padding: 10px;
            border-radius: 5px;
            margin-top: 10px;
            text-align: left;
        }}
        .info-box strong {{
            color: #1B5E20;
        }}
        .footer {{
            font-size: 14px;
            color: #777;
            margin-top: 20px;
        }}
        .button {{
            background-color: #2E7D32;
            color: white;
            padding: 12px 20px;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            display: inline-block;
            margin-top: 20px;
        }}
        .button:hover {{
            background-color: #1B5E20;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <p class='header'>✅ Lời nhắc đã được tạo thành công!</p>
        <p class='content'>
            Xin chào <strong>{user.FullName}</strong>,<br>
            Bạn vừa tạo một lời nhắc thành công trên hệ thống <strong>Pregnancy Growth Tracking</strong>.
        </p>
        <div class='info-box'>
            <p><strong>📌 Tiêu đề:</strong> {createdReminder.Title}</p>
            <p><strong>📖 Nội dung:</strong> {createdReminder.Notification}</p>
            <p><strong>🗂️ Loại nhắc nhở:</strong> {createdReminder.ReminderType}</p>
            <p><strong>🗓️ Thời gian:</strong> {createdReminder.Time} ngày {createdReminder.Date:dd/MM/yyyy}</p>
        </div>
        <p class='content'>
            Hệ thống sẽ gửi nhắc nhở cho bạn khi đến thời gian đã đặt. Vui lòng kiểm tra hộp thư của bạn!
        </p>
        <a class='button' href='https://your-website.com/reminders/{createdReminder.RemindId}'>Xem chi tiết</a>
        <p class='footer'>
            🤰 <strong>Pregnancy Growth Tracking</strong> luôn đồng hành cùng bạn trong hành trình làm mẹ!<br>
            <i>Liên hệ với chúng tôi nếu bạn cần hỗ trợ.</i>
        </p>
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
