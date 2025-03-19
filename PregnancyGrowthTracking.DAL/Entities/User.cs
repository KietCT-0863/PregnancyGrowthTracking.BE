using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Phone { get; set; }

    public bool? Available { get; set; }

    public int? RoleId { get; set; }

    public string? ProfileImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual ICollection<Foetus> Foetus { get; set; } = new List<Foetus>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();

    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<UserReminder> UserReminders { get; set; } = new List<UserReminder>();

}
