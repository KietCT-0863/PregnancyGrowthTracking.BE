using System.Text.Json.Serialization;

namespace PregnancyGrowthTracking.DAL.DTOs.Vnpay
{
    public class PaymentInformationModel
    {
        public string? OrderType { get; set; }
        [JsonIgnore]  // Bỏ qua amount từ request
        public decimal Amount { get; set; }  // Giữ nguyên là decimal
        public string? OrderDescription { get; set; }
        public string? Name { get; set; }
        public int UserId { get; set; }
        public int MembershipId { get; set; }
    }
}