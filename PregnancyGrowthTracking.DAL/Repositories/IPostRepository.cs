using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IPostRepository
    {
        Task<bool> AddPostAsync(Post post);

        Task<List<Post>> GetAllPostWithTagAsync();

        Task UpdatePostAsync(Post post);

        Task DeletePostAsync(Post post);

        Task<Post> GetPostByIdAsync(int postId);

        Task<Post> GetPostByTitleAndBodyAsync(string title, string body);
    }
}
