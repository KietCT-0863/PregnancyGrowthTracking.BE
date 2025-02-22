using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace PregnancyGrowthTracking.BLL.Services
{
    public interface IS3Service
    {
        Task<string> UploadFileAsync(IFormFile file, string userId);
        Task<List<string>> ListFilesAsync(string prefix);

    }
}
