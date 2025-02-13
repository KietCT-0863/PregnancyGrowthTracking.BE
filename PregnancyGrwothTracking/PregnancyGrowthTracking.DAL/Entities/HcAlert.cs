using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class HcAlert
{
    public int HcAlertsId { get; set; }

    public int? HcId { get; set; }

    public string? Notification { get; set; }

    public virtual HcStandard? Hc { get; set; }
}
