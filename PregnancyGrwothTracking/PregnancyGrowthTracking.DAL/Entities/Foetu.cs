using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Foetu
{
    public int FoetusId { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<GrowthCheck> GrowthChecks { get; set; } = new List<GrowthCheck>();

    public virtual User? User { get; set; }
}
