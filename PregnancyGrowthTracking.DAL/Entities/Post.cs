using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string PostTag { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<PostComment> PostComments { get; set; }

        public virtual ICollection<PostTag> PostTags { get; set; }
    }
}
