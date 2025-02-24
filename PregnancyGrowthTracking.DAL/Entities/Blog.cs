using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public string Body { get; set; } = null!;

    public virtual ICollection<BlogCate> BlogCate { get; set; } = new List<BlogCate>();
}
