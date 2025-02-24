using System.Threading.Tasks;
using PregnancyGrowthTracking.DAL.Entities;

namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IBlogRepository
    {
        Task<Category> GetCategoryByNameAsync(string categoryName);
    }
} 