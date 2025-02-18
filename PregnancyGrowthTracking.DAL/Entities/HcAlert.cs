using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class HcAlert
{
    public int HcAlertsId { get; set; }

    public int? GrowthStandardId { get; set; }

    public string? Notification { get; set; }

    public virtual GrowthStandard? GrowthStandard { get; set; }
}
