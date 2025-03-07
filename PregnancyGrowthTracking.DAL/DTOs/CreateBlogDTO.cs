using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreateBlogDTO
    {

        [Required(ErrorMessage = "Blog Title không được để trống.")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Blog Body không được để trống.")]
        public string Body { get; set; } = null!;

        [Required(ErrorMessage = "Category không được để trống.")]
        public List<CreateBlogCategoryDTO> Categories { get; set; } = new();

        public class CreateBlogCategoryDTO
        {
            [Required(ErrorMessage = "Category Name không được để trống.")]
            public string CategoryName { get; set; } = null!;
        }
    }
}
