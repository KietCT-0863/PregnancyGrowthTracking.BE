using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Foetu
{
    public int FoetusId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<GrowthDatum> GrowthData { get; set; } = new List<GrowthDatum>();

    public virtual User? User { get; set; }
}
