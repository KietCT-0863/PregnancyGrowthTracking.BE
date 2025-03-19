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
                .Include(c => c.User) //  Join bảng User để lấy thông tin
                .Select(c => new CommentResponseDto
                {
                    CommentId = c.CommentId,
                    PostId = c.PostId,
                    UserId = c.UserId ?? 0, // Nếu `null`, trả về 0
                    UserName = c.User != null ? c.User.FullName : "Unknown",
                    Comment = c.Comment,
                    CreatedDate = c.CreatedDate
                })
                .OrderByDescending(c => c.CreatedDate) //  Sắp xếp bình luận mới nhất lên đầu
                .ToListAsync();
        }
        public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _dbContext.PostComments
                .Where(c => c.PostId == postId) //  Chỉ lấy bình luận của bài viết này
                .Include(c => c.User)
                .Select(c => new CommentResponseDto
                {
                    CommentId = c.CommentId,
                    PostId = c.PostId,
                    UserId = c.UserId ?? 0, 
                    UserName = c.User != null ? c.User.FullName : "Unknown",
                    Comment = c.Comment,
                    CreatedDate = c.CreatedDate
                })
                .OrderByDescending(c => c.CreatedDate) //  Sắp xếp mới nhất trước
                .ToListAsync();
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
            _dbContext.PostComments.Remove(comment);
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}

