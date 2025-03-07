using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdateBlogDTO
    {
        [Required(ErrorMessage = "Blog Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Blog Id must be greater than or equal to 1.")]
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<UpdateBlogCategoryDTO> Categories { get; set; } = new();

        public class UpdateBlogCategoryDTO
        {
            public string CategoryName { get; set; } = null!;
        }
    }
}
