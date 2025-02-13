namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UserResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateOnly? Dob { get; set; }
        public bool Available { get; set; }
        public int RoleId { get; set; }
    }
}
