using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Membership
{
    public int MembershipId { get; set; }

    public string? Description { get; set; }

    public double? Price { get; set; }

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
