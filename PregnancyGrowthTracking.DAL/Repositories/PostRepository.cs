using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public PostRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AddPostAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
            return true; 
        }

        public async Task<List<Post>> GetAllPostWithTagAsync()
        {
            return await _dbContext.Posts
                .Where(p => p.IsActive)
                .Include(b => b.PostTags)
                .ThenInclude(bc => bc.Tag)
                .ToListAsync();
        }

        public async Task UpdatePostAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePostAsync(Post post)
        {
            _dbContext.Posts.Remove(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int postId)
        {
            return await _dbContext.Posts
                .Include(b => b.PostTags)
                .ThenInclude(bc => bc.Tag)
                .FirstOrDefaultAsync(b => b.PostId == postId);
        }

        public async Task<List<Post>> GetPostsByUserIdAsync(int userId)
        {
            return await _dbContext.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.PostTags)
                .ThenInclude(pt => pt.Tag)
                .ToListAsync();
        }

        public async Task<Post?> GetPostByTitleAndBodyAsync(string title, string body)
        {
            return await _dbContext.Posts
                .FirstOrDefaultAsync(b => b.Title == title && b.Body == body);
        }

        public async Task<bool> PostExistsAsync(int postId)
        {
            return await _dbContext.Posts.AnyAsync(p => p.PostId == postId);
        }
    }
}
