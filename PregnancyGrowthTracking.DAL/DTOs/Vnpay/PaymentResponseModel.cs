using System.Text.Json.Serialization;

namespace PregnancyGrowthTracking.DAL.DTOs.Vnpay
{
    public class PaymentResponseModel
    {
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }

        [JsonIgnore]
        public decimal Amount { get; set; } // Thêm Amount nếu cần
    }
}