using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IGrowthDataRepository
    {
        Task<int?> GetGrowthStandardIdByAgeAsync(int age);
        Task<bool> AddGrowthDataAsync(GrowthDatum growthData);
        Task<bool> HasGrowthDataAsync(int foetusId);
        Task<IEnumerable<GrowthDatum>> GetGrowthDataByFoetusIdAsync(int foetusId);
        Task<GrowthDatum?> GetGrowthDataByIdAsync(int growthDataId);
        Task<bool> UpdateGrowthDataAsync(GrowthDatum growthData);
        Task<GrowthDatum?> GetGrowthDataByUserAndAgeAsync(int userId, int age);
        Task<GrowthDatum?> GetGrowthDataByFoetusIdAndAge(int foetusId, int age);
    }
}
