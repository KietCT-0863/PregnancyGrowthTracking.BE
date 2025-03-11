using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdateReminderDto
    {

        public DateTime? Date { get; set; } // ❗ Cho phép null, giữ lại giá trị cũ nếu không nhập

        [RegularExpression(@"^(?:[01]\d|2[0-3]):[0-5]\d$", ErrorMessage = "Time must be in HH:mm format (24-hour clock).")]
        public string? Time { get; set; } // ❗ Cho phép null, giữ lại giá trị cũ nếu không nhập

        [MaxLength(255, ErrorMessage = "Title cannot exceed 255 characters.")]
        public string? Title { get; set; } // ❗ Cho phép null

        [MaxLength(500, ErrorMessage = "Notification cannot exceed 500 characters.")]
        public string? Notification { get; set; } // ❗ Cho phép null

        [MaxLength(50, ErrorMessage = "ReminderType cannot exceed 50 characters.")]
        public string? ReminderType { get; set; } // ❗ Cho phép null
    }
}
