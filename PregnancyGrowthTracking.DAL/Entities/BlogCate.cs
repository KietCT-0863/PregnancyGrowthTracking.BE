using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class BlogCate
{
    public int BlogCateId { get; set; }

    public int BlogId { get; set; }

    public int CategoryId { get; set; }

    public virtual Blog Blog { get; set; } = null!;

    public virtual Category Category { get; set; } = null!;
}