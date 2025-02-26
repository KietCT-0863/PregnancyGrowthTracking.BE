using PregnancyGrowthTracking.DAL.DTOs;
using PregnancyGrowthTracking.DAL.Entities;
using PregnancyGrowthTracking.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepo;

        public BlogService(IBlogRepository blogRepo)
        {
            _blogRepo = blogRepo;
        }

        // sử dụng lớp BlogDTO ở đây để tránh circular reference cũng như quản lý/giới hạn  thông tin mà client nhận được để tránh lộ thông tin nhạy cảm
        public async Task<List<BlogDTO>> GetAllBlogWithCateAsync()
        {
            List<Blog> listBlog = await _blogRepo.GetAllBlogWithCateAsync();

            // vỡi mỗi blog có trong listBlog sẽ tương ứng với 1 blog trong listBlogDTO
            List<BlogDTO> listBlogDTO = listBlog.Select(b => new BlogDTO
            {
                Title = b.Title,
                Body = b.Body,

                // với mỗi category trong BlogCate sẽ tương ứng với 1 category trong Categories
                Categories = b.BlogCate.Select(bc => new BlogDTO.BlogCategoryDTO
                {
                    CategoryName = bc.Category.CategoryName
                }).ToList()
            }).ToList();

            return listBlogDTO;
        }

        public async Task UpdateBlogAsync(BlogDTO blogDTO)
        {
            Blog existingBlog = await _blogRepo.GetBlogByIdAsync(blogDTO.Id);

            if (existingBlog == null)
            {
                return;
            }

            existingBlog.Title = blogDTO.Title ?? existingBlog.Title;
            existingBlog.Body = blogDTO.Body ?? existingBlog.Body;
            await _blogRepo.UpdateBlogAsync(existingBlog);

            if (blogDTO.Categories != null)
            {
                await UpdateBlogCateAsync(blogDTO);
            }
        }

        public async Task UpdateBlogCateAsync(BlogDTO blogDTO)
        {
            Blog existingBlog = await _blogRepo.GetBlogByIdAsync(blogDTO.Id);

            foreach (BlogCate blogCate in existingBlog.BlogCate.ToList())
            {
                await _blogRepo.RemoveBlogCateAsyns(blogCate);
            }

            foreach (var blogCate in blogDTO.Categories.ToList())
            {
                Category currentCate = await _blogRepo.GetCategoryByName(blogCate.CategoryName);

                BlogCate currentBlogCate = new BlogCate()
                {
                    BlogId = existingBlog.BlogId,
                    CategoryId = currentCate.CategoryId
                };

                await _blogRepo.AddBlogCateAsync(currentBlogCate);
            }
        }
    }
}
