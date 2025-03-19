using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface ICommentLikeService
    {
        Task<bool> ToggleLikeAsync(int commentId, int userId);
    }
}
