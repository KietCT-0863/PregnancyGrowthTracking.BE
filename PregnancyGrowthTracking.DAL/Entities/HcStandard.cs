using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class HcStandard
{
    public int HcId { get; set; }

    public int? StandardId { get; set; }

    public double? MedianValue { get; set; }

    public virtual ICollection<HcAlert> HcAlerts { get; set; } = new List<HcAlert>();

    public virtual Standard? Standard { get; set; }
}
