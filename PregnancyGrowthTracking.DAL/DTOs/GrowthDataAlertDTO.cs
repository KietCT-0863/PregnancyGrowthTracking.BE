using System;

namespace PregnancyGrowthTracking.DAL.DTOs
{
    public class GrowthDataAlertDTO
    {
        public bool IsAlert { get; set; }
        public double? CurrentValue { get; set; }
        public double? MinRange { get; set; }
        public double? MaxRange { get; set; }
    }
} 