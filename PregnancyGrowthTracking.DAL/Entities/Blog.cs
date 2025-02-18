using System;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Blog
{
    public int MediaId { get; set; }

    public string? Description { get; set; }

    public DateOnly? Date { get; set; }

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
