using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IGrowthStandardServices
    {
        Task<List<GrowthStandard>> GetGrowthStandardsAsync();

        void AddGrowthStandardAsync(GrowthStandard newGrowthStandard);

        void UpdateGrowthStandarddAsync(GrowthStandard growthStandard);
    }
}
