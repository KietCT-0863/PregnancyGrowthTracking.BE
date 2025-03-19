using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface ICommentLikeRepository
    {
        Task<CommentLike?> GetCommentLikeAsync(int commentId, int userId);
        Task<bool> AddLikeAsync(CommentLike commentLike);
        Task<bool> RemoveLikeAsync(CommentLike commentLike);
    }
}
