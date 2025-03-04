using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IGrowthDataService
    {
        Task<bool> AddGrowthDataAsync(int foetusId, GrowthDataCreateDto request);
        Task<IEnumerable<GrowthDataResponseDto>> GetGrowthDataByFoetusIdAsync(int foetusId, int userId);

        Task<bool> UpdateGrowthDataAsync(int userId, GrowthDataUpdateDto request);
    }
}

