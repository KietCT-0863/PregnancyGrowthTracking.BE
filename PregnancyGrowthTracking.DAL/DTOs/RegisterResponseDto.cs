namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class RegisterResponseDto
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public int? UserId { get; set; }
    }
}
