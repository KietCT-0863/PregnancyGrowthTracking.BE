using System;
using System.ComponentModel.DataAnnotations;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UpdateGrowthDataDto
    {
        [Required(ErrorMessage = "Tên thai nhi là bắt buộc.")]
        
        public string Name { get; set; }

        [Range(12, 40, ErrorMessage = "Tuổi thai chỉ được nhập từ tuần 12 đến tuần 40.")]
        public int? Age { get; set; }  // 🔹 Cho phép `null`, nếu không nhập sẽ giữ nguyên

        [Range(0, double.MaxValue, ErrorMessage = "HC không được là số âm và chỉ được nhập số.")]
        public double? HC { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "AC không được là số âm và chỉ được nhập số.")]
        public double? AC { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "FL không được là số âm và chỉ được nhập số.")]
        public double? FL { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "EFW không được là số âm và chỉ được nhập số.")]
        public double? EFW { get; set; }
    }
}
