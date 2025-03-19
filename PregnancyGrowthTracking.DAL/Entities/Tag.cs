using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string TagName { get; set; } = null!; // Đảm bảo không null

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>(); // Tránh lỗi null khi truy cập danh sách
}
