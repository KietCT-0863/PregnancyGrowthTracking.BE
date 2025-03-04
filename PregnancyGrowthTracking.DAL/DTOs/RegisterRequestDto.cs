using System.ComponentModel.DataAnnotations;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "Username là bắt buộc.")]
        [MinLength(4, ErrorMessage = "Username phải có ít nhất 4 ký tự.")]
        [MaxLength(20, ErrorMessage = "Username không được vượt quá 20 ký tự.")]
        [RegularExpression(@"^[a-zA-Z0-9_]+$", ErrorMessage = "Username chỉ được chứa chữ cái, số và dấu gạch dưới (_).")]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email là bắt buộc.")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$",
            ErrorMessage = "Mật khẩu phải chứa ít nhất một chữ cái thường, một chữ cái viết hoa và một chữ số.")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ và tên là bắt buộc.")]
        [MinLength(4, ErrorMessage = "Họ và tên phải có ít nhất 4 ký tự.")]
        [MaxLength(30, ErrorMessage = "Họ và tên không được quá 30 ký tự.")]
        [RegularExpression(@"^[a-zA-ZÀ-ỹ\s]+$", ErrorMessage = "Họ và tên chỉ được chứa chữ cái và khoảng trắng, không chứa số hoặc ký tự đặc biệt.")]
        public string FullName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Ngày sinh là bắt buộc.")]
        [DataType(DataType.Date)]
        public DateOnly Dob { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Số điện thoại phải có 10 chữ số và bắt đầu bằng 0.")]
        public string Phone { get; set; } = string.Empty;


    }
}
