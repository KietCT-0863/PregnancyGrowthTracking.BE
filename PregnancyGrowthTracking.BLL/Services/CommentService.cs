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
        private readonly IS3Service _s3Service;

        public CommentService(ICommentRepository commentRepository, IUserRepository userRepository,IS3Service s3Service)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _s3Service = s3Service;
        }

        public async Task<PostComment> CreateCommentAsync(CreateCommentDto request, int userId)
        {
            var newComment = new PostComment
            {
                PostId = request.PostId,
                UserId = userId,
                Comment = request.Comment,
                CreatedDate = DateTime.UtcNow,
                ParentCommentId = request.ParentCommentId
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
        public async Task<bool> UpdateCommentWithImageAsync(int commentId, int userId, UpdateCommentDto request)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(commentId);

            if (comment == null)
                throw new KeyNotFoundException("Bình luận không tồn tại.");

            if (comment.UserId != userId)
                throw new UnauthorizedAccessException("Bạn không có quyền sửa bình luận này.");

            // Cập nhật nội dung
            if (!string.IsNullOrWhiteSpace(request.Comment))
                comment.Comment = request.Comment;

            // Xử lý ảnh
            if (request.RemoveImage)
            {
                comment.CommentImageUrl = null;
            }
            else if (request.Image != null)
            {
                // Upload ảnh mới lên S3
                var imageUrl = await _s3Service.UploadFileAsync(request.Image, $"comments/{comment.PostId}");
                comment.CommentImageUrl = imageUrl;
            }

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

