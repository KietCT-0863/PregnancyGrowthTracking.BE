using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public class Membership
    {
        [Key]
        public int MembershipId { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        // Navigation property
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}

