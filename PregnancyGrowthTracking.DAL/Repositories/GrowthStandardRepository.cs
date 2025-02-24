using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class GrowthStandardRepository : IGrowthStandardRepository
    {
        private PregnancyGrowthTrackingDbContext? _dbContext;

        public async Task<List<GrowthStandard>> GetAllGrowthStandardAsync()
        {
            _dbContext = new();
            return await _dbContext.GrowthStandard.ToListAsync();
        }

        public async Task<bool> AddGrowthStandardAsync(GrowthStandard growthStandard)
        {
            _dbContext = new();
            _dbContext.GrowthStandard.Add(growthStandard);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateGrowthStandardAsync(GrowthStandard growthStandard)
        {
            _dbContext = new();
            _dbContext.GrowthStandard.Update(growthStandard);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<GrowthStandard?> GetGrowthStandardByAgeAsync(int? gestationalAge)
        {
            _dbContext = new();
            return await _dbContext.GrowthStandard.FirstOrDefaultAsync(gs => gs.GestationalAge == gestationalAge);
        }
    }
}
