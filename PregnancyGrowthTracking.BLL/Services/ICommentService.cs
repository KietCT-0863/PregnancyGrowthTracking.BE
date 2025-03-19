using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface ICommentService
    {
        Task<PostComment> CreateCommentAsync(CreateCommentDto request, int userId);
        Task<List<CommentResponseDto>> GetAllCommentsAsync();
        Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId);
        Task<bool> UpdateCommentAsync(int commentId, int userId, UpdateCommentDto request);
        Task<bool> DeleteCommentAsync(int commentId, int userId, bool isAdmin);
    }
}
