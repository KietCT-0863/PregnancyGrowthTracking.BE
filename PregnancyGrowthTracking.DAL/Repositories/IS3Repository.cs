using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;


namespace PregnancyGrowthTracking.DAL.Repositories
{
    public interface IS3Repository
    {
        Task<string> UploadFileAsync(IFormFile file, string userId);
        Task<List<string>> ListFilesAsync(string prefix);
    }
}
