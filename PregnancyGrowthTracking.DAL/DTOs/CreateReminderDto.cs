using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreateReminderDto
    {

        public DateTime? Date { get; set; }  // Ngày nhắc nhở
        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Time must be in HH:mm format (24-hour clock).")]
        public string? Time { get; set; }   // Giờ nhắc nhở (không bắt buộc)
        public string Title { get; set; }   // Tiêu đề nhắc nhở
        public string Notification { get; set; }  // Nội dung nhắc nhở
        public string ReminderType { get; set; }  // Loại nhắc nhở
    }
}
