using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IGrowthStandardRepository
    {
        Task<List<GrowthStandard>> GetAllGrowthStandardAsync();

        Task<bool> AddGrowthStandardAsync(GrowthStandard growthStandard);

        Task<GrowthStandard?> GetGrowthStandardByAgeAsync(int? gestationalAge);

        Task<bool> UpdateGrowthStandardAsync(GrowthStandard growthStandard);

    }

}
