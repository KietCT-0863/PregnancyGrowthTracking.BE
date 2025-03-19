using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class Post
{
    public int PostId { get; set; }
    public int UserId { get; set; }
    
    public string Title { get; set; } = null!;
    public string Body { get; set; } = null!;
    public string PostTag { get; set; } = null!;

    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }

    public virtual User User { get; set; } = null!;
    
    public virtual ICollection<PostComment> PostComments { get; set; } = new List<PostComment>();
    public virtual ICollection<PostLike> PostLikes { get; set; } = new List<PostLike>(); // Giữ thêm PostLikes từ remote
    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
