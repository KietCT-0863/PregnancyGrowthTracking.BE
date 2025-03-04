using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Username is required")]
        [MinLength(4, ErrorMessage = "Username must be at least 4 characters")]
        [MaxLength(30, ErrorMessage = "Username cannot exceed 30 characters")]
        [RegularExpression(@"^(?![0-9]+$)(?!.*\s)(?!.*[_\.]$)(?!^[_\.])(?!.*[_\.]{2,})[a-zA-Z0-9._]+$",
            ErrorMessage = "Username can only contain letters, numbers, underscores (_), or dots (.) but not at the beginning or end.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required")]
        [MinLength(4, ErrorMessage = "Full Name must be at least 4 characters")]
        [MaxLength(20, ErrorMessage = "Full Name cannot exceed 20 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Full Name can only contain letters and spaces")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        [MaxLength(30, ErrorMessage = "Password cannot exceed 30 characters")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]+$",
    ErrorMessage = "Password must contain at least one lowercase letter, one uppercase letter, and one number.")]
        public string Password { get; set; } = string.Empty;


        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        public DateOnly? Dob { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Phone number must be 10 digits and start with 0")]
        public string Phone { get; set; } = string.Empty;

        [Required]
        public bool? Available { get; set; }

        [Required(ErrorMessage = "RoleId is required")]
        [Range(1, 3, ErrorMessage = "RoleId must be between 1 and 3")]
        public int? RoleId { get; set; } = 2; // Mặc định là User
    }
}
