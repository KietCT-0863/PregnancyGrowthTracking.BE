﻿namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ProfileImageUrl { get; set; }
        public int UserId { get; set; }
        public string FullName { get; set; }
    }
}
