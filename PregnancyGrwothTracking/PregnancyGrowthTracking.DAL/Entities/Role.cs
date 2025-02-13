using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Role1 { get; set; }
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
