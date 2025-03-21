using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IPostTagRepository
    {
        Task AddPostTagAsync(PostTag postTag);

        Task RemovePostTagAsyns(PostTag postTag);
        Task<PostTag> GetPostTagByPostIdAndTagId(int postId, int tagId);
    }
}
