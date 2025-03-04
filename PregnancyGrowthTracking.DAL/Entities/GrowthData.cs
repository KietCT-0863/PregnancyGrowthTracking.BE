using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class GrowthDatum
{
    public int GrowthDataId { get; set; }

    public int? FoetusId { get; set; }

    public DateOnly? Date { get; set; }

    public int? GrowthStandardId { get; set; }

    public int? Age { get; set; }

    public double? Hc { get; set; }

    public double? Ac { get; set; }

    public double? Fl { get; set; }

    public double? Efw { get; set; }

    public virtual Foetus? Foetus { get; set; }

    public virtual GrowthStandard? GrowthStandard { get; set; }
}
