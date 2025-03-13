using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class UserProfileResponseDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public DateOnly? Dob { get; set; }
        public string? Phone { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
