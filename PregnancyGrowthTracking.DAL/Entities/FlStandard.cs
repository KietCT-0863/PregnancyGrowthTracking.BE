using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class FlStandard
{
    public int FlId { get; set; }

    public int? StandardId { get; set; }

    public double? MedianValue { get; set; }

    public virtual ICollection<FlAlert> FlAlerts { get; set; } = new List<FlAlert>();

    public virtual Standard? Standard { get; set; }
}
