using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class AcAlert
{
    public int AcAlertsId { get; set; }

    public int? AcId { get; set; }

    public string? Notification { get; set; }

    public virtual AcStandard? Ac { get; set; }
}
