using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IGrowthDataService
    {
        Task<bool> IsAddOrUpdate(int foetusId, int userId, GrowthDataDto request);

        Task<bool> AddGrowthDataAsync(int foetusId, GrowthDataDto request);

        Task<IEnumerable<GrowthDataWithAlertResponseDto>> GetGrowthDataByFoetusIdAsync(int foetusId, int userId);

        Task<bool> UpdateGrowthDataAsync(int userId, GrowthDataDto request);

        Task<Dictionary<string, GrowthDataAlertDTO>> AlertReturnWithRange(GrowthDataDto growthData);

    }
}

