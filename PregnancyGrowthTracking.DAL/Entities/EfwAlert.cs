using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class EfwAlert
{
    public int EfwAlertsId { get; set; }

    public int? GrowthStandardId { get; set; }

    public string? Notification { get; set; }

    public virtual GrowthStandard? GrowthStandard { get; set; }
}
