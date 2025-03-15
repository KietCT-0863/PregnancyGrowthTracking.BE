using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PostComment
{
    public int CommentId { get; set; }

    public string Comment { get; set; } = null!;

    public int PostId { get; set; }

    public int UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual Post Post { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
