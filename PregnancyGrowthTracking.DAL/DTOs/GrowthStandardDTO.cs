using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class GrowthStandardDTO
    {
        // Data Annotations - Chú thích dữ liệu

        [Required(ErrorMessage = "Gestational Age is required.")]
        [Range(12, 40, ErrorMessage = "Gestational Age must be between 12 and 40 weeks.")]
        public int? GestationalAge { get; set; }

        [Required(ErrorMessage = "HcMedian is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "HcMedian must be a positive number.")]
        public double? HcMedian { get; set; }

        [Required(ErrorMessage = "AcMedian is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "AcMedian must be a positive number.")]
        public double? AcMedian { get; set; }

        [Required(ErrorMessage = "FlMedian is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "FlMedian must be a positive number.")]
        public double? FlMedian { get; set; }

        [Required(ErrorMessage = "EfwMedian is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "EfwMedian must be a positive number.")]
        public double? EfwMedian { get; set; }
    }
}
