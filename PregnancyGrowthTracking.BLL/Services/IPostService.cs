using Microsoft.AspNetCore.Http;
using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IPostService
    {
        Task<List<PostDto>> GetAllPostWithIdAsync();

        Task UpdatePostAsync(UpdatePostDto postDto);

        Task AddPostAsync(int userId, CreatePostDto postDto);

        Task DeletePostAsync(int postID);

        Task<PostDto> GetPostByIdAsync(int postId);

        Task RemoveTagFromPostAsync(int postId, string tagName);

        Task DeactivatePostAsync(int postID);
    }
}
