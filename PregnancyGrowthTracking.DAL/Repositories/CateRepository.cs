using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class CateRepository : ICateRepository
    {
        private PregnancyGrowthTrackingDbContext? _dbContext;

        public async Task<Category> GetCategoryByName(string cateName)
        {
            _dbContext = new();
            return await _dbContext.Categorie.FirstOrDefaultAsync(c => c.CategoryName == cateName);
        }
    }
}
