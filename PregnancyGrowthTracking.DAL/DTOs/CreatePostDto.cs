using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static PregnancyGrowthTracking.DAL.DTOs.CreateBlogDTO;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreatePostDto
    {
        [JsonIgnore]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Title là bắt buộc")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Body là bắt buộc")]
        public string Body { get; set; } = null!;

        [MaxLength(2, ErrorMessage = "Chỉ được phép thêm tối đa 2 tags")]
        public List<CreatePostTagDTO> CreatePostTag { get; set; } = new();

        public class CreatePostTagDTO
        {
            public string TagName { get; set; } = null!;
        }
    }
}
