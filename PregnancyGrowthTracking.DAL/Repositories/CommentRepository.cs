using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public CommentRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PostComment> AddCommentAsync(PostComment comment)
        {
            _dbContext.PostComments.Add(comment);
            await _dbContext.SaveChangesAsync();
            return comment;
        }
        public async Task<List<CommentResponseDto>> GetAllCommentsAsync()
        {
            return await _dbContext.PostComments
                .Include(c => c.User)
                .Select(c => new CommentResponseDto
                {
                    CommentId = c.CommentId,
                    PostId = c.PostId,
                    UserId = c.UserId ?? 0,
                    UserName = c.User != null ? c.User.FullName : "Unknown",
                    Comment = c.Comment,
                    CreatedDate = c.CreatedDate,
                    ParentCommentId = c.ParentCommentId,
                    CommentImageUrl = c.CommentImageUrl
                })
                .OrderByDescending(c => c.CreatedDate)
                .ToListAsync();
        }

        public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
        {
            var comments = await _dbContext.PostComments
                .Where(c => c.PostId == postId)
                .Include(c => c.User)
                .Include(c => c.Replies) 
                .ToListAsync();

            
            return comments.Select(c => new CommentResponseDto
            {
                CommentId = c.CommentId,
                PostId = c.PostId,
                UserId = c.UserId ?? 0,
                UserName = c.User?.FullName ?? "Unknown",
                Comment = c.Comment,
                CreatedDate = c.CreatedDate,
                ParentCommentId = c.ParentCommentId,
                CommentImageUrl = c.CommentImageUrl

            }).OrderByDescending(c => c.CreatedDate).ToList();
        }
        public async Task<PostComment?> GetCommentByIdAsync(int commentId)
        {
            return await _dbContext.PostComments.FirstOrDefaultAsync(c => c.CommentId == commentId);
        }

        public async Task<bool> UpdateCommentAsync(PostComment comment)
        {
            _dbContext.PostComments.Update(comment);
            return await _dbContext.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteCommentAsync(PostComment comment)
        {
            
            var replies = await _dbContext.PostComments
                .Where(c => c.ParentCommentId == comment.CommentId)
                .ToListAsync();

            _dbContext.PostComments.RemoveRange(replies);
            _dbContext.PostComments.Remove(comment);

            return await _dbContext.SaveChangesAsync() > 0;
        }

    }
}

