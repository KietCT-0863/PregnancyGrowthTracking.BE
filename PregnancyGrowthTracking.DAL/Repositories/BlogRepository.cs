﻿using Amazon.S3.Model.Internal.MarshallTransformations;
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
        private readonly PregnancyGrowthTrackingDbContext _dbContext;

        public BlogRepository(PregnancyGrowthTrackingDbContext dbContext)
        {
            _dbContext = dbContext; // Khởi tạo _dbContext thông qua constructor injection
        }

        public async Task<bool> AddBlogAsync(Blog blog)
        {
            _dbContext.Blog.Add(blog);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Blog>> GetAllBlogWithCateAsync()
        {
            return await _dbContext.Blog
                .Include(b => b.BlogCates)
                .ThenInclude(bc => bc.Category)
                .ToListAsync();
        }

        public async Task UpdateBlogAsync(Blog blog)
        {
            _dbContext.Blog.Update(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteBlogAsync(Blog blog)
        {
            _dbContext.Blog.Remove(blog);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Blog> GetBlogByIdAsync(int blogId)
        {
            return await _dbContext.Blog
                .Include(b => b.BlogCates)
                .ThenInclude(bc => bc.Category)
                .FirstOrDefaultAsync(b => b.BlogId == blogId);
        }

        public async Task<Blog> GetBlogByTitleAndBodyAsync(string title, string body)
        {
            return await _dbContext.Blog
                .FirstOrDefaultAsync(b => b.Title == title && b.Body == body);
        }
    }
}