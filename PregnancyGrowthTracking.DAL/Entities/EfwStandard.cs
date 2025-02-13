using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public partial class EfwStandard
    {
        [Key]  // ✅ Xác định đây là khóa chính
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ✅ Nếu cần tự động tăng
        [Column("EFW_Id")]
        public int EfwId { get; set; }

        public int? StandardId { get; set; }

        public double? MedianValue { get; set; }

        [ForeignKey("StandardId")]
        public virtual Standard? Standard { get; set; }

        public virtual ICollection<EfwAlert> EfwAlerts { get; set; } = new List<EfwAlert>();
    }
}
