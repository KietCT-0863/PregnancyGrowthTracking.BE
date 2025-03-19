using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PostTag
{
    public int PostTagId { get; set; }

    public int PostId { get; set; }

    public int TagId { get; set; }

    public virtual Post Post { get; set; } = null!; // Đảm bảo không null

    public virtual Tag Tag { get; set; } = null!; // Đảm bảo không null
}
