using S3Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.services
{
    public interface IS3Service
    {
        Task<S3Response> CreateBucketAsync(string bucketName);
        Task UploadFileSync(string bucketName);
        Task<List<TranAnxDto>> GetObjectFromS3Async(string backName);
    }  
}
