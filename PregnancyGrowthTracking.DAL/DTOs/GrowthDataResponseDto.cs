using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class GrowthDataResponseDto
    {
        public int GrowthDataId { get; set; }
        public int Age { get; set; }
        public double HC { get; set; }
        public double AC { get; set; }
        public double FL { get; set; }
        public double EFW { get; set; }
        public int? GrowthStandardId { get; set; }
        public string Date { get; set; }
    }
}
