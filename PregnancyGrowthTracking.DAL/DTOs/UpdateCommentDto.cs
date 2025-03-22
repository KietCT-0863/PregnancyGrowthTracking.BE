using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdateCommentDto
    {
        public string? Comment { get; set; }
        public IFormFile? Image { get; set; }
        public bool RemoveImage { get; set; } = false;
    }
}
