using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class ForumTag
{
    public int TagId { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<UserForum> UserForums { get; set; } = new List<UserForum>();
}
