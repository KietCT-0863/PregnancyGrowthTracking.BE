using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Foetu
{
    public int FoetusId { get; set; }

    public int? UserId { get; set; }

    public string? Name { get; set; }

    public int? GestationAge { get; set; }

    public DateOnly? ExpectedBirthDate { get; set; }

    public string? Gender { get; set; }

    public virtual ICollection<GrowthDatum> GrowthData { get; set; } = new List<GrowthDatum>();

    public virtual User? User { get; set; }
}
