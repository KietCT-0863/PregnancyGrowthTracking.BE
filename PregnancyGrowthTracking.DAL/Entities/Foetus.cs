using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Foetus
{
    public int FoetusId { get; set; }

    public int? UserId { get; set; }

    public DateOnly? Edd { get; set; }

    public bool IsBorn { get; set; }

    public virtual ICollection<GrowthData> GrowthData { get; set; } = new List<GrowthData>();

    public virtual User? User { get; set; }
}
