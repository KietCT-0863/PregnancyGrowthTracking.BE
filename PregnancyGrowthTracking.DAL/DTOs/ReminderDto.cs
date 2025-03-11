using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class ReminderDto
    {
        public int RemindId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string Title { get; set; }
        public string Notification { get; set; }
        public string ReminderType { get; set; }
        public bool IsEmailSent { get; set; }
    }
}
