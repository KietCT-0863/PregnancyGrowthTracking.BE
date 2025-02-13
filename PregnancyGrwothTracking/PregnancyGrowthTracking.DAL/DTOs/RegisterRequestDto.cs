using System.ComponentModel.DataAnnotations;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Username phải có ít nhất 6 ký tự.")]
        [MaxLength(20, ErrorMessage = "Username không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username chỉ được chứa chữ cái, số và dấu gạch dưới (_).")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{6,}$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái và một số.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        [MaxLength(50, ErrorMessage = "Họ và tên không được quá 50 ký tự.")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.")]
        public string Phone { get; set; } = string.Empty;


    }
}
