using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
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
        private PregnancyGrowthTrackingDbContext _dbContext;

        public async Task<List<GrowthStandard>> GetAllGrowthStandardAsync()
        {
            _dbContext = new();
            return await _dbContext.GrowthStandards.ToListAsync();
        }

        public async Task<bool> AddGrowthStandardAsync(GrowthStandard newGrowthStandard)
        {
            _dbContext = new();
            _dbContext.GrowthStandards.Add(newGrowthStandard);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateGrowthStandardAsync(GrowthStandard growthStandard)
        {
            _dbContext = new();
            _dbContext.Update(growthStandard);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
