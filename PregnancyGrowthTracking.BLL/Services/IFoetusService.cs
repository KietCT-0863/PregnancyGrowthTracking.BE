using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IFoetusService
    {
        Task<FoetusResponseDto> CreateFoetusAsync(int userId, FoetusCreateDto request);
        Task<IEnumerable<FoetusResponseDto>> GetFoetusesByUserIdAsync(int userId);
        Task<bool> DeleteFoetusAsync(int userId, int foetusId);
    }
}
