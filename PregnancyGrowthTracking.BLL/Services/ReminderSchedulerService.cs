using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.BLL.Services;

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
                using (var scope = _scopeFactory.CreateScope()) // ✅ Tạo scope mới để lấy DbContext
                {
                    try
                    {
                        Console.WriteLine("🔍 [CHECK] Đang kiểm tra Reminder trước 1 tiếng...");

                        // ✅ Lấy `DbContext` từ scope
                        var dbContext = scope.ServiceProvider.GetRequiredService<PregnancyGrowthTrackingDbContext>();
                        var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                        var reminders = await dbContext.UserReminders
                            .Include(r => r.User) // ✅ Đảm bảo lấy thông tin User
                            .Where(r => !r.IsEmailSent && r.Date == DateTime.UtcNow.Date)
                            .ToListAsync();

                        foreach (var reminder in reminders)
                        {
                            var user = reminder.User;
                            if (user == null || string.IsNullOrEmpty(user.Email))
                                continue;

                            Console.WriteLine($"📧 [SENDING] Sending email to {user.Email} for Reminder: {reminder.Title}");

                            // 📌 **Tạo tiêu đề & nội dung email**
                            string subject = $"🔔 Nhắc nhở trước 1 tiếng: {reminder.Title}";
                            string body = $@"
<html>
<head>
    <style>
        body {{
            font-family: Arial, sans-serif;
            background-color: #f0f8ff;
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
            color: #1976D2;
            margin-bottom: 10px;
        }}
        .info-box {{
            background-color: #E3F2FD;
            padding: 12px;
            border-radius: 8px;
            margin-top: 10px;
            text-align: left;
            border-left: 5px solid #1976D2;
        }}
        .button {{
            background-color: #FF9800;
            color: white;
            padding: 12px 20px;
            text-decoration: none;
            border-radius: 5px;
            font-size: 16px;
            display: inline-block;
            margin-top: 20px;
            font-weight: bold;
        }}
        .button:hover {{
            background-color: #F57C00;
        }}
    </style>
</head>
<body>
    <div class='email-container'>
        <p class='header'>⏳ Nhắc nhở quan trọng!</p>
        <p>Xin chào <strong>{user.FullName}</strong>,</p>
        <div class='info-box'>
            <p><strong>📌 Tiêu đề:</strong> {reminder.Title}</p>
            <p><strong>📖 Nội dung:</strong> {reminder.Notification}</p>
            <p><strong>🗓️ Ngày:</strong> {reminder.Date:dd/MM/yyyy}</p>
            <p><strong>🕒 Giờ:</strong> {reminder.Time}</p>
        </div>
        <a class='button' href='https://your-website.com/reminders/{reminder.RemindId}'>Xem chi tiết</a>
    </div>
</body>
</html>";

                            // 📌 **Gửi email**
                            bool emailSent = await emailService.SendEmailAsync(user.Email, subject, body);

                            if (emailSent)
                            {
                                Console.WriteLine($"✅ [EMAIL SENT] Reminder '{reminder.Title}' sent to {user.Email}");

                                // ✅ Cập nhật IsEmailSent = true trong database
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

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); // Kiểm tra lại sau 10 phút
            }
        }
    }
}
