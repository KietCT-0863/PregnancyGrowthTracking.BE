using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class ReminderSchedulerService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReminderSchedulerService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🔄 [BACKGROUND SERVICE] ReminderSchedulerService đang chạy...");

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    try
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<PregnancyGrowthTrackingDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        // ✅ Lấy giờ Việt Nam
                        var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                        var nowUtc = DateTime.UtcNow;
                        var nowVietnam = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
                        var targetTime = nowVietnam.AddHours(1).TimeOfDay; // ✅ Giờ 1 tiếng sau

                        Console.WriteLine($"⏳ [DEBUG] Giờ hiện tại Việt Nam: {nowVietnam}");
                        Console.WriteLine($"⏳ [DEBUG] Giờ Reminder cần gửi trước 1 tiếng: {targetTime}");

                        // ✅ Lọc reminders trước 1 tiếng (±5 phút)
                        var reminders = await dbContext.UserReminders
                            .Include(r => r.User)
                            .Where(r => !r.IsEmailSent && r.Date == nowVietnam.Date)
                            .ToListAsync();

                        var filteredReminders = reminders
                            .Where(r =>
                            {
                                if (string.IsNullOrWhiteSpace(r.Time) || !TimeSpan.TryParse(r.Time, out TimeSpan reminderTime))
                                {
                                    Console.WriteLine($"❌ [WARNING] Reminder {r.RemindId} có `Time` sai định dạng: {r.Time}");
                                    return false;
                                }

                                bool shouldSend = Math.Abs(reminderTime.TotalMinutes - targetTime.TotalMinutes) <= 5;
                                Console.WriteLine($"🔍 [DEBUG] Kiểm tra Reminder: {r.Title} | Time: {r.Time} | Gửi: {shouldSend}");
                                return shouldSend;
                            })
                            .ToList();

                        Console.WriteLine($"🔍 [DEBUG] Số Reminder cần gửi: {filteredReminders.Count}");

                        foreach (var reminder in filteredReminders)
                        {
                            var user = reminder.User;
                            if (user == null || string.IsNullOrEmpty(user.Email))
                                continue;

                            Console.WriteLine($"📧 [SENDING] Sending email to {user.Email} for Reminder: {reminder.Title}");

                            string subject = $"⏳ Nhắc nhở trước 1 tiếng: {reminder.Title}";
                            string body = $@"
<!DOCTYPE html>
<html lang='vi'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>⏳ Nhắc Nhở Quan Trọng</title>
    <style>
        body {{
            font-family: 'Arial', sans-serif;
            background-color: #fff3e0;
            margin: 0;
            padding: 20px;
        }}
        .email-container {{
            background-color: #ffffff;
            max-width: 600px;
            margin: auto;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
            text-align: center;
            border: 1px solid #ffcc80;
        }}
        .header {{
            font-size: 24px;
            font-weight: bold;
            color: #e65100;
            padding: 15px;
            background-color: #ffe0b2;
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
            background-color: #fff8e1;
            padding: 15px;
            border-radius: 8px;
            margin-top: 15px;
            text-align: left;
            border-left: 5px solid #ff9800;
        }}
        .info-box strong {{
            color: #e65100;
        }}
        .button {{
            display: inline-block;
            background-color: #ff9800;
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
            background-color: #f57c00;
        }}
        .footer {{
            font-size: 14px;
            color: #777;
            padding-top: 20px;
            border-top: 1px solid #ddd;
        }}
        .highlight {{
            font-weight: bold;
            color: #e65100;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <div class='header'>⏳ Nhắc Nhở Quan Trọng!</div>

        <div class='content'>
            <p>Xin chào <strong>{user.FullName}</strong>,</p>
            <p>Bạn có một nhắc nhở quan trọng trong vòng <strong>1 giờ tới</strong>. Hãy kiểm tra chi tiết bên dưới:</p>

            <div class='info-box'>
                <p><strong>📌 Tiêu đề:</strong> {reminder.Title}</p>
                <p><strong>📖 Nội dung:</strong> {reminder.Notification}</p>
                <p><strong>🗓️ Ngày:</strong> {reminder.Date:dd/MM/yyyy}</p>
                <p><strong>⏰ Giờ:</strong> {reminder.Time}</p>
            </div>

            <p>Hãy sẵn sàng và đảm bảo bạn không bỏ lỡ!</p>

           <a class='button' href='http://localhost:5173/member/calendar-detail/{reminder.RemindId}'>📅 Xem chi tiết</a>
        </div>

        <p class='footer'>
             <strong>Pregnancy Growth Tracking</strong> giúp bạn luôn nhớ các sự kiện quan trọng!<br>
            <i>Liên hệ với chúng tôi nếu bạn cần hỗ trợ.</i>
        </p>
    </div>
</body>
</html>";

                            bool emailSent = await emailService.SendEmailAsync(user.Email, subject, body);

                            if (emailSent)
                            {
                                Console.WriteLine($"✅ [EMAIL SENT] Reminder '{reminder.Title}' sent to {user.Email}");
                                reminder.IsEmailSent = true;
                                dbContext.UserReminders.Update(reminder);
                                await dbContext.SaveChangesAsync();
                                Console.WriteLine($"✅ [SUCCESS] Đã cập nhật IsEmailSent = true trong database!");
                            }
                            else
                            {
                                Console.WriteLine($"❌ [EMAIL FAILED] Could not send email to {user.Email}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"❌ [ERROR] Lỗi trong Background Service: {ex.Message}");
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
