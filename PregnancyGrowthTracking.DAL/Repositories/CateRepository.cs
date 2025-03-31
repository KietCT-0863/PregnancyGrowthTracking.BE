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
        private PregnancyGrowthTrackingDbContext _dbContext;

        public CateRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetCategoryByName(string cateName)
        {
            return await _dbContext.Categorie.FirstOrDefaultAsync(c => c.CategoryName == cateName);
        }

        public async Task<List<Category>> GetAllCategory()
        {
            return await _dbContext.Categorie.ToListAsync();
        }
    }
}
