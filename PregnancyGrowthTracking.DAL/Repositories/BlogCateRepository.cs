using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class BlogCateRepository : IBlogCateRepository
    {
        private PregnancyGrowthTrackingDbContext? _dbContext;

        public async Task AddBlogCateAsync(BlogCate blogCate)
        {
            _dbContext = new();
            _dbContext.BlogCate.Add(blogCate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveBlogCateAsyns(BlogCate blogCate)
        {
            _dbContext = new();
            _dbContext.BlogCate.Remove(blogCate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
