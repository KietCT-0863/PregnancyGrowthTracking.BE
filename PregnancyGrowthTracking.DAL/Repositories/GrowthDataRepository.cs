using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class GrowthDataRepository : IGrowthDataRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public GrowthDataRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int?> GetGrowthStandardIdByAgeAsync(int age)
        {
            var standard = await _dbContext.GrowthStandards
                .Where(gs => gs.GestationalAge == age)
                .FirstOrDefaultAsync();

            return standard?.GrowthStandardId;
        }

        public async Task<bool> AddGrowthDataAsync(GrowthDatum growthData)
        {
            _dbContext.GrowthData.Add(growthData);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> HasGrowthDataAsync(int foetusId)
        {
            return await _dbContext.GrowthData.AnyAsync(gd => gd.FoetusId == foetusId);
        }

        public async Task<IEnumerable<GrowthDatum>> GetGrowthDataByFoetusIdAsync(int foetusId)
        {
            return await _dbContext.GrowthData
                .Where(gd => gd.FoetusId == foetusId)
                .Include(gd => gd.GrowthStandard) // ✅ Lấy thông tin chuẩn phát triển
                .OrderBy(gd => gd.Date) // ✅ Sắp xếp theo ngày nhập dữ liệu
                .ToListAsync();
        }

        public async Task<GrowthDatum?> GetGrowthDataByIdAsync(int growthDataId)
        {
            return await _dbContext.GrowthData
                .Include(gd => gd.Foetus)
                .FirstOrDefaultAsync(gd => gd.GrowthDataId == growthDataId);
        }

        public async Task<bool> UpdateGrowthDataAsync(GrowthDatum growthData)
        {
            _dbContext.GrowthData.Update(growthData);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<GrowthDatum?> GetGrowthDataByUserAndAgeAsync(int userId, int age)
        {
            return await _dbContext.GrowthData
                .Include(gd => gd.Foetus)
                .Where(gd => gd.Foetus.UserId == userId && gd.Age == age)
                .FirstOrDefaultAsync();
        }

        public async Task<GrowthDatum?> GetGrowthDataByFoetusIdAndAge(int foetusId, int age)
            => await _dbContext.GrowthData.FirstOrDefaultAsync(gd => gd.FoetusId == foetusId && gd.Age == age);
    }
}
