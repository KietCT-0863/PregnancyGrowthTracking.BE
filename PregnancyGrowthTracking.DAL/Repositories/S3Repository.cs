using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using PregnancyGrowthTracking.DAL.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace PregnancyGrowthTracking.DAL.Repositories
{
    public class S3Repository : IS3Repository
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Repository(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:BucketName"]
                ?? throw new ArgumentNullException("AWS:BucketName không được tìm thấy trong cấu hình.");
        }

        public async Task<string> UploadFileAsync(IFormFile file, string userId)
        {
            var fileName = $"{userId}/{Guid.NewGuid()}_{file.FileName}";

            using var stream = file.OpenReadStream();
            var request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = _bucketName,
                ContentType = file.ContentType,
                //CannedACL = S3CannedACL.PublicRead
            };

            await _s3Client.PutObjectAsync(request);
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }

        public async Task<List<string>> ListFilesAsync(string prefix)
        {
            try
            {
                var request = new ListObjectsV2Request
                {
                    BucketName = _bucketName,
                    Prefix = prefix
                };

                var response = await _s3Client.ListObjectsV2Async(request);

                // ✅ Kiểm tra nếu không có file
                return response.S3Objects?
                           .Select(o => $"https://{_bucketName}.s3.amazonaws.com/{o.Key}")
                           .ToList() ?? new List<string>();
            }
            catch (AmazonS3Exception ex) when (ex.ErrorCode == "NoSuchBucket")
            {
                Console.WriteLine("AWS S3 Bucket không tồn tại hoặc đã bị xóa.");
                return new List<string> { "Bucket đã bị xóa hoặc không tồn tại." };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi liệt kê file từ S3: {ex.Message}");
                return new List<string> { "Đã xảy ra lỗi khi kết nối S3." };
            }
        }

    }
}