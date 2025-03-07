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
    public interface IBlogService
    {
        Task<List<BlogDTO>> GetAllBlogWithCateAsync();

        Task UpdateBlogAsync(BlogDTO blogDTO);

        Task AddBlogAsync(CreateBlogDTO blogDTO);

        Task DeleteBlogAsync(int blogID);

        Task<BlogDTO> GetBlogByIdAsync(int blogId);
        Task UploadPhotoAsync(int blogId, IFormFile file);

        Task ReplacePhotoAsync(int blogId, IFormFile file);
    }
}
