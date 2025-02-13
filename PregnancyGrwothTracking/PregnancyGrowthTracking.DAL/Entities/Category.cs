using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Category1 { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();
}
