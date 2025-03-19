using System;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;

        public PostLikeService(IPostLikeRepository postLikeRepository)
        {
            _postLikeRepository = postLikeRepository;
        }

        public async Task LikePostAsync(int postId, int userId)
        {
            var existingLike = await _postLikeRepository.GetLikeAsync(postId, userId);
            if (existingLike != null)
                throw new InvalidOperationException("Bạn đã like bài viết này rồi");

            await _postLikeRepository.CreateAsync(new PostLike
            {
                PostId = postId,
                UserId = userId
            });
        }

        public async Task UnlikePostAsync(int postId, int userId)
        {
            var existingLike = await _postLikeRepository.GetLikeAsync(postId, userId);
            if (existingLike == null)
                throw new InvalidOperationException("Bạn chưa like bài viết này");

            await _postLikeRepository.DeleteAsync(existingLike);
        }

        public async Task<int> GetLikesCountAsync(int postId)
        {
            return await _postLikeRepository.GetLikesCountAsync(postId);
        }
    }
}