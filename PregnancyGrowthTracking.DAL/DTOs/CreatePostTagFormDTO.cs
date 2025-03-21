using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreatePostTagFormDTO
    {
        [MaxLength(2, ErrorMessage = "Chỉ được phép thêm tối đa 2 tags")]
        public List<CreatePostTagForm> Tags { get; set; } = new();

        public class CreatePostTagForm
        {
            [Required(ErrorMessage = "TagName là bắt buộc")]
            public string TagName { get; set; } = null!;
        }
    }
}
