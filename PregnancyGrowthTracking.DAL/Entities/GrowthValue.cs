using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class GrowthValue
{
    public int GrowthValueId { get; set; }

    public int? GrowthCheckId { get; set; }

    public int? StandardId { get; set; }

    public int? Age { get; set; }

    public double? Hc { get; set; }

    public double? Ac { get; set; }

    public double? Fl { get; set; }

    public double? Efw { get; set; }

    public virtual GrowthCheck? GrowthCheck { get; set; }

    public virtual Standard? Standard { get; set; }
}
