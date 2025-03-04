using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class GrowthDataCreateDto
    {
        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 42, ErrorMessage = "Age must be between 1 and 42 weeks.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "HC is required.")]
        public double HC { get; set; }

        [Required(ErrorMessage = "AC is required.")]
        public double AC { get; set; }

        [Required(ErrorMessage = "FL is required.")]
        public double FL { get; set; }

        [Required(ErrorMessage = "EFW is required.")]
        public double EFW { get; set; }
    }
}
