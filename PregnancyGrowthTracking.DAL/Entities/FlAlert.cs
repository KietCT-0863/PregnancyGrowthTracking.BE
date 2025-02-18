using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class FlAlert
{
    public int FlAlertsId { get; set; }

    public int? GrowthStandardId { get; set; }

    public string? Notification { get; set; }

    public virtual GrowthStandard? GrowthStandard { get; set; }
}
