using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities;

public partial class PostComment
{
    public int CommentId { get; set; }

    public string Comment { get; set; } = null!;

    public int PostId { get; set; }

    public int? UserId { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CommentImageUrl { get; set; }

    // Thêm thuộc tính để hỗ trợ reply
    public int? ParentCommentId { get; set; }

    public virtual PostComment? ParentComment { get; set; } // Self-reference

    public virtual ICollection<PostComment> Replies { get; set; } = new List<PostComment>(); // Các comment reply

    public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();

    public virtual Post Post { get; set; } = null!;

    public virtual User? User { get; set; }
}
