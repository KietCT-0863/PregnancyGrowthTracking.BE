using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PostComment
{
    public int CommentId { get; set; }

    public string Comment { get; set; } = null!; // Đảm bảo không null

    public int PostId { get; set; }

    public int? UserId { get; set; } // Giữ nullable để tránh lỗi khi UserId có thể rỗng

    public DateTime CreatedDate { get; set; }

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; } // Đổi thành `User?` để phù hợp với `UserId?`
}
