using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class PostTagRepository : IPostTagRepository
    {
        private PregnancyGrowthTrackingDbContext? _dbContext;

        public async Task AddPostTagAsync(PostTag postTag)
        {
            _dbContext = new();
            _dbContext.PostTags.Add(postTag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task RemovePostTagAsyns(PostTag postTag)
        {
            _dbContext = new();
            _dbContext.PostTags.Remove(postTag);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<PostTag> GetPostTagByPostIdAndTagId(int postId, int tagId)
        {
            return await _dbContext.PostTags.FirstOrDefaultAsync(pt => pt.PostId == postId && pt.TagId == tagId);
        }
    }
}
