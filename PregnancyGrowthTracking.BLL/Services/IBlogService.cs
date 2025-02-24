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

        Task UpdateBlogAsync(int id, BlogDTO blogDTO);
    }
}
