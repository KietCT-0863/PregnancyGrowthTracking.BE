using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class GrowthDataUpdateDto
    {
        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 50, ErrorMessage = "Age must be between 1 and 50.")]
        public int Age { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "HC must be a positive number.")]
        public double? HC { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "AC must be a positive number.")]
        public double? AC { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FL must be a positive number.")]
        public double? FL { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "EFW must be a positive number.")]
        public double? EFW { get; set; }
    }
}
