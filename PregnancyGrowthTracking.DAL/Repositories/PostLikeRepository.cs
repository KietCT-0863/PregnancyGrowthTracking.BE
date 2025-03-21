using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class PostLikeRepository : IPostLikeRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _context;

        public PostLikeRepository(PregnancyGrowthTrackingDbContext context)
        {
            _context = context;
        }

        public async Task<PostLike> GetLikeAsync(int postId, int userId)
        {
            return await _context.PostLikes
                .FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
        }

        public async Task CreateAsync(PostLike postLike)
        {
            await _context.PostLikes.AddAsync(postLike);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(PostLike postLike)
        {
            _context.PostLikes.Remove(postLike);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _context.PostLikes.CountAsync(pl => pl.PostId == postId);
        }

        public async Task<bool> PostExistsAsync(int postId)
        {
            return await _context.Posts.AnyAsync(p => p.PostId == postId);
        }
    }
}