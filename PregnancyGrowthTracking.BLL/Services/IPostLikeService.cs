using System.Security.Claims;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IPostLikeService
    {
        Task<int> ToggleLikePostAsync(int postId, ClaimsPrincipal user);
        Task<int> GetLikesCountAsync(int postId);
    }
}
