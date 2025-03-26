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

        Task<Post?> GetPostByIdAsync(int postId);

        Task<List<Post>> GetPostsByUserIdAsync(int userId);

        Task<Post?> GetPostByTitleAndBodyAsync(string title, string body);

        Task<Post> GetPostWithAuthorAsync(int postId);

        Task<bool> PostExistsAsync(int postId);
    }
}
