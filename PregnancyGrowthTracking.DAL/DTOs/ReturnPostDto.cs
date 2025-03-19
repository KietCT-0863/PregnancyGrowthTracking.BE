using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class ReturnPostDto
    {
        [Required(ErrorMessage = "Post Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Post Id must be greater than or equal to 1.")]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<string> PostTags { get; set; } = new();

        public string PostImageUrl { get; set; } = null!;

        public DateTime CreatedDate { get; set; }
        public class PostTagDTO
        {
            public string TagName { get; set; } = null!;
        }
    }
}
