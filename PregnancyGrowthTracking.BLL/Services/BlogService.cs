using Amazon.S3.Model;
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
        private readonly IBlogCateRepository _blogCateRepo;
        private readonly ICateRepository _cateRepo;

        public BlogService(IBlogRepository blogRepo, IBlogCateRepository blogCateRepo, ICateRepository cateRepo)
        {
            _blogRepo = blogRepo;
            _blogCateRepo = blogCateRepo;
            _cateRepo = cateRepo;
        }

        // sử dụng lớp BlogDTO ở đây để tránh circular reference cũng như quản lý/giới hạn  thông tin mà client nhận được để tránh lộ thông tin nhạy cảm
        public async Task<List<BlogDTO>> GetAllBlogWithCateAsync()
        {
            List<Blog> listBlog = await _blogRepo.GetAllBlogWithCateAsync();

            // vỡi mỗi blog có trong listBlog sẽ tương ứng với 1 blog trong listBlogDTO
            List<BlogDTO> listBlogDTO = listBlog.Select(b => new BlogDTO
            {
                Id = b.BlogId,
                Title = b.Title,
                Body = b.Body,

                // với mỗi category trong BlogCate sẽ tương ứng với 1 category trong Categories
                Categories = b.BlogCates.Select(bc => new BlogDTO.BlogCategoryDTO
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
            Category? currentCate = null;
            List<BlogCate> currentListBlogCate = new();

            foreach (var blogCate in blogDTO.Categories.ToList())
            {
                if (blogCate.CategoryName == "")
                {
                    return;
                }

                currentCate = await _cateRepo.GetCategoryByName(blogCate.CategoryName);

                currentListBlogCate.Add(new BlogCate()
                {
                    BlogId = existingBlog.BlogId,
                    CategoryId = currentCate.CategoryId
                });
            }

            if (currentListBlogCate != null)
            {
                foreach (BlogCate blogCate in existingBlog.BlogCates.ToList())
                {
                    await _blogCateRepo.RemoveBlogCateAsyns(blogCate);
                }
            }

            foreach (BlogCate? blogCate in currentListBlogCate)
            {
                if (blogCate != null)
                {
                    await _blogCateRepo.AddBlogCateAsync(blogCate);
                }
            }
        }

        public async Task AddBlogAsync(CreateBlogDTO blogDTO)
        {
            Blog blog = new Blog
            {
                Title = blogDTO.Title,
                Body = blogDTO.Body,
            };

            await _blogRepo.AddBlogAsync(blog);

            Blog newBlog = await _blogRepo.GetBlogByTitleAndBodyAsync(blog.Title, blog.Body);

            foreach (var blogcate in blogDTO.Categories)
            {
                Category newCate = await _cateRepo.GetCategoryByName(blogcate.CategoryName);
                BlogCate newBlogCate = new BlogCate
                {
                    BlogId = newBlog.BlogId,
                    CategoryId = newCate.CategoryId
                };

                await _blogCateRepo.AddBlogCateAsync(newBlogCate);
            }
        }

        public async Task DeleteBlogAsync(int blogID)
        {
            if (blogID < 0)
            {
                throw new ArgumentException("Blog Id phải lớn hơn 1");
            }

            Blog currentBlog = await _blogRepo.GetBlogByIdAsync(blogID);

            if (currentBlog == null)
            {
                throw new Exception();
            }

            foreach (BlogCate blogCate in currentBlog.BlogCates.ToList())
            {
                await _blogCateRepo.RemoveBlogCateAsyns(blogCate);
            }

            await _blogRepo.DeleteBlogAsync(currentBlog);
        }
    }
}
