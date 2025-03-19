using PregnancyGrowthTracking.DAL.Entities;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IPostLikeRepository
    {
        Task<PostLike> GetLikeAsync(int postId, int userId);
        Task CreateAsync(PostLike postLike);
        Task DeleteAsync(PostLike postLike);
        Task<int> GetLikesCountAsync(int postId);
    }
}