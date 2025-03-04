using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class FoetusResponseDto
    {
        public int FoetusId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int? GestationalAge { get; set; }
        [JsonIgnore]
        public DateTime? ExpectedBirthDate { get; set; }

        public string? ExpectedBirthDateFormatted => ExpectedBirthDate?.ToString("yyyy-MM-dd");
        public int UserId { get; set; }

    }
}
