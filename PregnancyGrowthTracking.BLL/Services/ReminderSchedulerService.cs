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

                            string subject = $"🔔 Nhắc nhở trước 1 tiếng: {reminder.Title}";
                            string body = $@"
                            <html>
                            <body style='font-family: Arial, sans-serif; background-color: #f0f8ff; padding: 20px;'>
                                <div style='background-color: #ffffff; padding: 20px; border-radius: 10px; box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.1); max-width: 600px; margin: auto; text-align: center;'>
                                    <p style='font-size: 22px; font-weight: bold; color: #1976D2;'>⏳ Nhắc nhở quan trọng!</p>
                                    <p>Xin chào <strong>{user.FullName}</strong>,</p>
                                    <p><strong>📌 Tiêu đề:</strong> {reminder.Title}</p>
                                    <p><strong>📖 Nội dung:</strong> {reminder.Notification}</p>
                                    <p><strong>🗓️ Ngày:</strong> {reminder.Date:dd/MM/yyyy}</p>
                                    <p><strong>🕒 Giờ:</strong> {reminder.Time}</p>
                                    <a href='https://your-website.com/reminders/{reminder.RemindId}' style='display: inline-block; background-color: #FF9800; color: white; padding: 12px 20px; text-decoration: none; border-radius: 5px; font-size: 16px; font-weight: bold;'>Xem chi tiết</a>
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
