using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public class PostComment
    {
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<CommentLike> CommentLikes { get; set; } = new List<CommentLike>();
    }
}

