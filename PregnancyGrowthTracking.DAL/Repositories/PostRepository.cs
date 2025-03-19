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
            try
            {
                await _dbContext.Posts.AddAsync(post);
                var result = await _dbContext.SaveChangesAsync();
                return result > 0;
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException;
                throw new Exception($"Lỗi khi thêm post vào database: {innerException?.Message ?? ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi không xác định khi thêm post: {ex.Message}", ex);
            }
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

        public async Task<Post> GetPostByIdAsync(int postId)
        {
            return await _dbContext.Posts
                .Include(b => b.PostTags)
                .ThenInclude(bc => bc.Tag)
                .FirstOrDefaultAsync(b => b.PostId == postId);
        }

        public async Task<Post> GetPostByTitleAndBodyAsync(string title, string body)
        {
            return await _dbContext.Posts
                .FirstOrDefaultAsync(b => b.Title == title && b.Body == body);
        }
    }
}
