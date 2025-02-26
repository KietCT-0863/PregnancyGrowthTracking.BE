using Amazon.S3.Model.Internal.MarshallTransformations;
using Microsoft.EntityFrameworkCore;
using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private PregnancyGrowthTrackingDbContext? _dbContext;

        public async Task<bool> AddBlogAsync(Blog blog)
        {
            _dbContext = new();
            _dbContext.Blog.Add(blog);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Blog>> GetAllBlogWithCateAsync()
        {
            _dbContext = new();
            return await _dbContext.Blog.Include(b => b.BlogCate).ThenInclude(bc => bc.Category).ToListAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _dbContext = new();
            _dbContext.Blog.Update(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            _dbContext = new();
            return await _dbContext.Blog.Include(b => b.BlogCate).ThenInclude(bc => bc.Category).FirstOrDefaultAsync(b => b.BlogId == blogId);
        }

        public async Task AddBlogCateAsync(BlogCate blogCate)
        {
            _dbContext = new();
            _dbContext.BlogCates.Add(blogCate);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByName(string cateName)
        {
            _dbContext = new();
            return await _dbContext.Categories.FirstOrDefaultAsync(c => c.CategoryName == cateName);
        }

        public async Task RemoveBlogCateAsyns(BlogCate blogCate)
        {
            _dbContext= new();
            _dbContext.BlogCates.Remove(blogCate);
            await _dbContext.SaveChangesAsync();
        }
    }
}
