using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
        }

        public async Task<PostComment> CreateCommentAsync(CreateCommentDto request, int userId)
        {
            var newComment = new PostComment
            {
                PostId = request.PostId,
                UserId = userId, // ✅ Lấy từ Controller (JWT)
                Comment = request.Comment,
                CreatedDate = DateTime.UtcNow
            };

            return await _commentRepository.AddCommentAsync(newComment);
        }
        public async Task<List<CommentResponseDto>> GetAllCommentsAsync()
        {
            return await _commentRepository.GetAllCommentsAsync();
        }
        public async Task<List<CommentResponseDto>> GetCommentsByPostIdAsync(int postId)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(postId);
        }
        public async Task<bool> UpdateCommentAsync(int commentId, int userId, UpdateCommentDto request)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null)
                throw new KeyNotFoundException("Bình luận không tồn tại.");

            if (comment.UserId != userId)
                throw new UnauthorizedAccessException("Bạn không có quyền sửa bình luận này.");

            // Nếu người dùng để trống `Comment`, giữ nguyên dữ liệu cũ
            comment.Comment = string.IsNullOrWhiteSpace(request.Comment) ? comment.Comment : request.Comment;

            return await _commentRepository.UpdateCommentAsync(comment);
        }
        public async Task<bool> DeleteCommentAsync(int commentId, int userId, bool isAdmin)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null)
                throw new KeyNotFoundException("Bình luận không tồn tại.");

            // Chỉ chủ sở hữu hoặc admin mới có quyền xóa
            if (!isAdmin && comment.UserId != userId)
                throw new UnauthorizedAccessException("Bạn không có quyền xóa bình luận này.");

            return await _commentRepository.DeleteCommentAsync(comment);
        }
    }
}

