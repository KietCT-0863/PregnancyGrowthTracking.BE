using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class CreateCommentDto
    {
        public int PostId { get; set; }
        public string Comment { get; set; } = null!;

        public int? ParentCommentId { get; set; }

        public IFormFile? Image { get; set; }
    }
}
