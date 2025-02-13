using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PregnancyGrowthTracking.DAL.Entities
{
    public class Payment
    {
        [Key]
        public int? PaymentId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int MembershipId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [ForeignKey("MembershipId")]
        public virtual Membership? Membership { get; set; }
    }
}