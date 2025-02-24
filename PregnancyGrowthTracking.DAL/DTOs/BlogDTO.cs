namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class BlogDTO
    {
        public int Id { get; set; } = 0;
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public List<BlogCategoryDTO> Categories { get; set; } = new();

        public class BlogCategoryDTO
        {
            public string CategoryName { get; set; } = null!;
        }
    }
} 