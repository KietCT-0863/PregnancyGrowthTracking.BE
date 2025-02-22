using PregnancyGrowthTracking.BLL.Services;
using PregnancyGrowthTracking.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace PregnancyGrowthTracking.BLL.Services
{
    public class S3Service : IS3Service
    {
        private readonly IS3Repository _repository;
        private readonly string _bucketName;

        public S3Service(IS3Repository repository, IConfiguration configuration)
        {
            _repository = repository;
            _bucketName = configuration["AWS:BucketName"]
                           ?? throw new ArgumentNullException("AWS:BucketName không được tìm thấy trong cấu hình.");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string userId)
        {
            return await _repository.UploadFileAsync(file, userId);
        }

        public async Task<List<string>> ListFilesAsync(string prefix)
        {
            return await _repository.ListFilesAsync(prefix);
        }
    }
}


