using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class CommentLike
{
    public int CommentLikeId { get; set; }

    public int CommentId { get; set; }

    public int UserId { get; set; }

    public virtual PostComment Comment { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
