using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface ICommentRepository
    {
        Task<PostComment> AddCommentAsync(PostComment comment);
        Task<List<CommentResponseDto>> GetAllCommentsAsync();
        Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId);
        Task<PostComment?> GetCommentByIdAsync(int commentId);
        Task<bool> UpdateCommentAsync(PostComment comment);
        Task<bool> DeleteCommentAsync(PostComment comment);
    }
}
