namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UserUpdateDto : UserCreateDto
    {
        // Loại bỏ [Required] để cho phép cập nhật một phần
        public new string? UserName { get; set; }
        public new string? FullName { get; set; }
        public new string? Email { get; set; }
        public new string? Password { get; set; }
        public new DateOnly? Dob { get; set; }
        public new string? Phone { get; set; }
        public new bool? Available { get; set; }
        public new int? RoleId { get; set; }
    }
}
