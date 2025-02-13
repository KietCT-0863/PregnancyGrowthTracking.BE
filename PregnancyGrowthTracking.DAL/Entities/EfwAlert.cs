using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public partial class EfwAlert
    {
        [Key]  // ✅ Xác định đây là Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // ✅ Tự động tăng nếu cần
        public int EfwAlertsId { get; set; }

        public int? EfwId { get; set; }

        public string? Notification { get; set; }

        [ForeignKey("EfwId")]
        public virtual EfwStandard? Efw { get; set; }
    }
}
