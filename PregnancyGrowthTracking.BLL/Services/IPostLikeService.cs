using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IPostLikeService
    {
        Task LikePostAsync(int postId, int userId);
        Task UnlikePostAsync(int postId, int userId);
        Task<int> GetLikesCountAsync(int postId);
    }
}
