using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class CommentLikeService : ICommentLikeService
    {
        private readonly ICommentLikeRepository _commentLikeRepository;

        public CommentLikeService(ICommentLikeRepository commentLikeRepository)
        {
            _commentLikeRepository = commentLikeRepository;
        }

        public async Task<bool> ToggleLikeAsync(int commentId, int userId)
        {
            if (_commentLikeRepository == null)
                throw new InvalidOperationException("_commentLikeRepository chưa được khởi tạo!");

            var existingLike = await _commentLikeRepository.GetCommentLikeAsync(commentId, userId);

            if (existingLike != null)
            {
                Console.WriteLine("Đã tìm thấy like, tiến hành Unlike...");
                await _commentLikeRepository.RemoveLikeAsync(existingLike);
                return false; // Unlike
            }

            Console.WriteLine("Không tìm thấy like, tiến hành Like...");
            var newLike = new CommentLike
            {
                CommentId = commentId,
                UserId = userId
            };

            await _commentLikeRepository.AddLikeAsync(newLike);
            return true; // Like
        }
    }
}

