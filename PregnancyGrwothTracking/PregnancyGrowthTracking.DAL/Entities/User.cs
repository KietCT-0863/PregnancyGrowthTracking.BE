using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }
    [MaxLength(64)]
    public string? Password { get; set; }

    public DateOnly? Dob { get; set; }

    public string? Phone { get; set; }

    public bool? Available { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<Foetu> Foetus { get; set; } = new List<Foetu>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    [JsonIgnore]
    public virtual Role? Role { get; set; }

    public virtual ICollection<UserComment> UserComments { get; set; } = new List<UserComment>();

    public virtual ICollection<UserForum> UserForums { get; set; } = new List<UserForum>();

    public virtual ICollection<UserNote> UserNotes { get; set; } = new List<UserNote>();

    public virtual ICollection<UserReminder> UserReminders { get; set; } = new List<UserReminder>();
}
