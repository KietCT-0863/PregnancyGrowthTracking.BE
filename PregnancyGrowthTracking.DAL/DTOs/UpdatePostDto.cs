using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PregnancyGrowthTracking.DAL.DTOs.UpdateBlogDTO;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdatePostDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Body { get; set; }
        public List<UpdatePostTagDTO>? Tags { get; set; }

        public class UpdatePostTagDTO
        {
            public string TagName { get; set; } = null!;
        }
    }
}
