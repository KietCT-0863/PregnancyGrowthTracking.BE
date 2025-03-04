using PregnancyGrowthTracking.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IBlogCateRepository
    {
        Task AddBlogCateAsync(BlogCate blogCate);

        Task RemoveBlogCateAsyns(BlogCate blogCate);
    }
}
