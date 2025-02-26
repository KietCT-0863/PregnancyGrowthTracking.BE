using System.ComponentModel.DataAnnotations;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class BlogDTO
    {
        [Required(ErrorMessage = "Blog Id is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Blog Id must be greater than or equal to 1.")]
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<BlogCategoryDTO> Categories { get; set; } = new();

        public class BlogCategoryDTO
        {
            public string CategoryName { get; set; } = null!;
        }
    }
} 