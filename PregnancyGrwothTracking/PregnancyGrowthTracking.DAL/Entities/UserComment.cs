using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class UserComment
{
    public int CommentId { get; set; }

    public int? UserId { get; set; }

    public int? BlogId { get; set; }

    public string? Detail { get; set; }

    public DateOnly? Date { get; set; }

    public virtual UserForum? Blog { get; set; }

    public virtual User? User { get; set; }
}
