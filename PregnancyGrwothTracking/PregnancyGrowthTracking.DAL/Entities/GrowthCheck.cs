using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class GrowthCheck
{
    public int GrowthCheckId { get; set; }

    public int? FoetusId { get; set; }

    public DateOnly? Date { get; set; }

    public virtual Foetu? Foetus { get; set; }

    public virtual ICollection<GrowthValue> GrowthValues { get; set; } = new List<GrowthValue>();
}
