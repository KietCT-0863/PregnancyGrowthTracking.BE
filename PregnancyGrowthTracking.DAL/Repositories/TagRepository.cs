using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public TagRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tag> GetTagByName(string tagName)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(c => c.TagName == tagName);
        }

        public async Task<int> AddTagAsync(Tag tag)
        {
            _dbContext.Tags.Add(tag);
            await _dbContext.SaveChangesAsync();
            return tag.TagId;
        }
    }
}
