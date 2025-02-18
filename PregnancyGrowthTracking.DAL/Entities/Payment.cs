using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Payment
{
    public int PaymentId { get; set; }

    public int? UserId { get; set; }

    public int? MembershipId { get; set; }

    public DateOnly? Date { get; set; }

    public double? TotalPrice { get; set; }

    public virtual Membership? Membership { get; set; }

    public virtual User? User { get; set; }
}
