using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public ReminderRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserReminder> CreateReminderAsync(UserReminder reminder)
        {
            await _dbContext.UserReminders.AddAsync(reminder);
            await _dbContext.SaveChangesAsync();
            return reminder;
        }

        public async Task<IEnumerable<UserReminder>> GetReminderHistoryAsync(int userId)
        {
            return await _dbContext.UserReminders
                .Where(r => r.UserId == userId)
                .OrderByDescending(r => r.Date) // 🔹 Sắp xếp theo ngày giảm dần
                .ToListAsync();
        }
        public async Task<bool> DeleteReminderAsync(int userId, int remindId)
        {
            var reminder = await _dbContext.UserReminders
                .FirstOrDefaultAsync(r => r.RemindId == remindId && r.UserId == userId);

            if (reminder == null)
                return false; // 🔹 Không tìm thấy hoặc không thuộc về user

            _dbContext.UserReminders.Remove(reminder);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateReminderAsync(int userId, int remindId, UpdateReminderDto request)
        {
            var reminder = await _dbContext.UserReminders
                .FirstOrDefaultAsync(r => r.RemindId == remindId && r.UserId == userId);

            if (reminder == null)
                return false; // 🔹 Không tìm thấy hoặc không thuộc về user

            // ❗ Kiểm tra nếu `Date` nhỏ hơn hôm nay thì báo lỗi
            if (request.Date.HasValue && request.Date.Value.Date < DateTime.UtcNow.Date)
            {
                throw new ArgumentException("Date must be today or in the future.");
            }

            // ❗ Kiểm tra `Time` có đúng định dạng `HH:mm` không
            if (!string.IsNullOrWhiteSpace(request.Time) && !Regex.IsMatch(request.Time, @"^(?:[01]\d|2[0-3]):[0-5]\d$"))
            {
                throw new ArgumentException("Time must be in HH:mm format (24-hour clock).");
            }

            // ❗ Giữ lại giá trị cũ nếu không nhập dữ liệu mới
            reminder.Date = request.Date ?? reminder.Date;
            reminder.Time = !string.IsNullOrWhiteSpace(request.Time) ? request.Time : reminder.Time;
            reminder.Title = !string.IsNullOrWhiteSpace(request.Title) ? request.Title : reminder.Title;
            reminder.Notification = !string.IsNullOrWhiteSpace(request.Notification) ? request.Notification : reminder.Notification;
            reminder.ReminderType = !string.IsNullOrWhiteSpace(request.ReminderType) ? request.ReminderType : reminder.ReminderType;

            _dbContext.UserReminders.Update(reminder);
            await _dbContext.SaveChangesAsync();
            return true;
        }


        public async Task<IEnumerable<UserReminder>> GetRemindersToSendAsync()
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vietnamTimeZone); // ✅ Giờ Việt Nam

            return await _dbContext.UserReminders
                .Include(r => r.User)
                .Where(r => !r.IsEmailSent && r.Date == now.Date && r.Time == now.ToString("HH:mm"))
                .ToListAsync();
        }


        public async Task<IEnumerable<UserReminder>> GetRemindersBeforeOneHourAsync()
        {
            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var nowUtc = DateTime.UtcNow;
            var nowVietnam = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone); // ✅ Chuyển sang giờ Việt Nam
            var targetTime = nowVietnam.AddHours(1).TimeOfDay; // ✅ Thời gian 1 tiếng sau

            Console.WriteLine($"⏳ [DEBUG] Giờ hiện tại Việt Nam: {nowVietnam}");
            Console.WriteLine($"⏳ [DEBUG] Giờ Reminder cần gửi trước 1 tiếng: {targetTime}");

            var reminders = await _dbContext.UserReminders
                .Include(r => r.User) // ✅ Lấy dữ liệu User để gửi email
                .Where(r => !r.IsEmailSent && r.Date == nowVietnam.Date)
                .ToListAsync(); // ✅ Chuyển sang danh sách trước khi lọc

            var filteredReminders = reminders
                .Where(r =>
                {
                    if (string.IsNullOrWhiteSpace(r.Time) || !Regex.IsMatch(r.Time, @"^\d{2}:\d{2}$"))
                    {
                        Console.WriteLine($"❌ [WARNING] Reminder {r.RemindId} có `Time` sai định dạng: {r.Time}");
                        return false;
                    }

                    TimeSpan reminderTime;
                    bool isValidTime = TimeSpan.TryParse(r.Time, out reminderTime);

                    if (!isValidTime)
                    {
                        Console.WriteLine($"❌ [WARNING] Không thể parse `Time`: {r.Time}");
                        return false;
                    }

                    bool shouldSend = Math.Abs(reminderTime.TotalMinutes - targetTime.TotalMinutes) <= 5;

                    Console.WriteLine($"🔍 [DEBUG] Kiểm tra Reminder: {r.Title} | Time: {r.Time} | Gửi: {shouldSend}");

                    return shouldSend;
                })
                .ToList();

            // 🔥 **Thêm Log để kiểm tra Reminder được chọn**
            Console.WriteLine($"🔍 [DEBUG] Số Reminder tìm thấy trước 1 tiếng: {filteredReminders.Count}");
            foreach (var r in filteredReminders)
            {
                Console.WriteLine($"📩 [DEBUG] Reminder sẽ gửi trước 1 tiếng:");
                Console.WriteLine($"- ID: {r.RemindId}");
                Console.WriteLine($"- Title: {r.Title}");
                Console.WriteLine($"- Time: {r.Time}");
                Console.WriteLine($"- ReminderType: {r.ReminderType}");
                Console.WriteLine($"- Notification: {r.Notification}");
            }

            return filteredReminders;
        }




    }
}
