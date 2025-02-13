using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Standard
{
    public int StandardId { get; set; }

    public int? GestationalAge { get; set; }

    public virtual ICollection<AcStandard> AcStandards { get; set; } = new List<AcStandard>();

    public virtual ICollection<EfwStandard> EfwStandards { get; set; } = new List<EfwStandard>();

    public virtual ICollection<FlStandard> FlStandards { get; set; } = new List<FlStandard>();

    public virtual ICollection<GrowthValue> GrowthValues { get; set; } = new List<GrowthValue>();

    public virtual ICollection<HcStandard> HcStandards { get; set; } = new List<HcStandard>();
}
