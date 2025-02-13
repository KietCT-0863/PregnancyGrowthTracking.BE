using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class UserReminder
{
    public int RemindId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Date { get; set; }

    public string? Notification { get; set; }

    public virtual User? User { get; set; }
}
