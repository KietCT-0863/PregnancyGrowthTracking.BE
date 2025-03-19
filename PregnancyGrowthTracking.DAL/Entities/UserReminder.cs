using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class UserReminder
{
    public int RemindId { get; set; }

    public int? UserId { get; set; }

    public DateTime? Date { get; set; }

    public string? Notification { get; set; }

    public string Title { get; set; } = null!;

    public string? ReminderType { get; set; }

    public bool IsEmailSent { get; set; }

    public string? Time { get; set; }

    public virtual User? User { get; set; }
}
