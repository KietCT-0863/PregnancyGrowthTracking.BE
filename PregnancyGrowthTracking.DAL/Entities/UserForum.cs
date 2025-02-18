using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class UserForum
{
    public int BlogId { get; set; }

    public int? UserId { get; set; }

    public string? Detail { get; set; }

    public DateOnly? Date { get; set; }

    public int? TagId { get; set; }

    public virtual ForumTag? Tag { get; set; }

    public virtual User? User { get; set; }

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();
}
