using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public partial class Blog
    {
        [Key]  // ✅ Định nghĩa khóa chính
        public int MediaId { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Description { get; set; }

        public DateOnly? Date { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }
    }
}
