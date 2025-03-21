using System;
using System.Security.Claims;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IPostRepository _postRepository;

        public PostLikeService(IPostLikeRepository postLikeRepository, IPostRepository postRepository)
        {
            _postLikeRepository = postLikeRepository;
            _postRepository = postRepository;
        }

        public async Task<int> ToggleLikePostAsync(int postId, ClaimsPrincipal user)
        {
            if (user == null)
                throw new UnauthorizedAccessException("Người dùng chưa đăng nhập!");
            if (!await _postRepository.PostExistsAsync(postId))
                throw new ArgumentException("Bài viết không tồn tại!");

            // Lấy userId từ token
            var userIdClaim = user.FindFirst("UserId"); 
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
                throw new UnauthorizedAccessException("Không thể xác định ID người dùng!");

            var existingLike = await _postLikeRepository.GetLikeAsync(postId, userId);

            if (existingLike == null)
            {
                // Nếu chưa like thì tạo mới
                await _postLikeRepository.CreateAsync(new PostLike
                {
                    PostId = postId,
                    UserId = userId
                });
            }
            else
            {
                await _postLikeRepository.DeleteAsync(existingLike);
            }

            return await _postLikeRepository.GetLikesCountAsync(postId);
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _postLikeRepository.GetLikesCountAsync(postId);
        }

    }
}