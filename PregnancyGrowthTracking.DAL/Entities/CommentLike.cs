using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public class CommentLike
    {
        public int CommentLikeId { get; set; }
        public int CommentId { get; set; }
        public int UserId { get; set; }

        public virtual PostComment Comment { get; set; }
        public virtual User User { get; set; }
    }

}
