using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IBlogRepository
    {
        Task<bool> AddBlogAsync(Blog blog);

        Task<List<Blog>> GetAllBlogWithCateAsync();

        Task UpdateBlogAsync(Blog blog);

        Task<Blog> GetBlogByIdAsync(int blogId);

        Task AddBlogCateAsync(BlogCate blogCate);

        Task<Category> GetCategoryByName(string cateName);

        Task RemoveBlogCateAsyns(BlogCate blogCate);
    }
}
