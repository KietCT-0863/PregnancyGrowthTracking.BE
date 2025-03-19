using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PostLike
{
    public int PostLikeId { get; set; }

    public int PostId { get; set; }

    public int UserId { get; set; }

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
