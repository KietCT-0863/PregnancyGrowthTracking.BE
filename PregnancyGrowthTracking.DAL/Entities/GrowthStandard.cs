using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class GrowthStandard
{
    public int GrowthStandardId { get; set; }

    public int? GestationalAge { get; set; }

    public double? HcMedian { get; set; }

    public double? AcMedian { get; set; }

    public double? FlMedian { get; set; }

    public double? EfwMedian { get; set; }

    public virtual ICollection<GrowthDatum> GrowthData { get; set; } = new List<GrowthDatum>();
}
