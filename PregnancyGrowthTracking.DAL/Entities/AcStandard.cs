using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class AcStandard
{
    public int AcId { get; set; }

    public int? StandardId { get; set; }

    public double? MedianValue { get; set; }

    public virtual ICollection<AcAlert> AcAlerts { get; set; } = new List<AcAlert>();

    public virtual Standard? Standard { get; set; }
}
