using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class CommentLikeRepository : ICommentLikeRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public CommentLikeRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CommentLike?> GetCommentLikeAsync(int commentId, int userId)
        {
            return await _dbContext.CommentLikes
                .FirstOrDefaultAsync(cl => cl.CommentId == commentId && cl.UserId == userId);
        }

        public async Task<bool> AddLikeAsync(CommentLike commentLike)
        {
            await _dbContext.CommentLikes.AddAsync(commentLike);
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveLikeAsync(CommentLike commentLike)
        {
            _dbContext.CommentLikes.Remove(commentLike);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
