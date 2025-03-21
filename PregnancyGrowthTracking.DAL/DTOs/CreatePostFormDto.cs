using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreatePostFormDto
    {
        [Required(ErrorMessage = "Title là bắt buộc")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Body là bắt buộc")]
        public string Body { get; set; } = null!;

        public IFormFile? PostImage { get; set; }
    }
}