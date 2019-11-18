using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using S3Test.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.services
{
    public class S3Service  : IS3Service
    {
        private readonly IAmazonS3 _client;

        public S3Service(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<S3Response> CreateBucketAsync(string bucketName)
        {
            try
            {
                var res = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);
                if (res == false)
                {
                    var putBucketRequest = new PutBucketRequest
                    {
                        BucketName = bucketName,
                        UseClientRegion  = true
                    };
                   var  response = await _client.PutBucketAsync(putBucketRequest);
                    return new S3Response
                {
                    Message = response.ResponseMetadata.RequestId,
                    status = response.HttpStatusCode
                };
                }
               
            }
            catch (AmazonS3Exception e)
            {
                return new S3Response
                {
                    status = e.StatusCode,
                    Message = e.Message
                };
            }
            catch (Exception e)
            {

                return new S3Response
                {
                    status = System.Net.HttpStatusCode.InternalServerError  ,
                    Message = e.Message
                };
            }
            return new S3Response
            {
                status = System.Net.HttpStatusCode.InternalServerError,
                Message = "something went wrong"
            };
        }

        private const string FilePath = "C:\\Steps.txt";
        private const string UploadWithKeyName = "UploadWithKeyName";
        private const string FileStreamUpload = "FileStreamUpload";
        private const string AdvancedUpload = "AdvancedUpload";

        public async Task UploadFileSync(string bucketName)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(_client);
                //option 1

                await fileTransferUtility.UploadAsync(FilePath, bucketName);
                //option 2
                await fileTransferUtility.UploadAsync(FilePath, bucketName, UploadWithKeyName);
                //option 3 
                using (var fileTouplaod = new FileStream(FilePath,FileMode.Open,FileAccess.Read))
                {
                    await fileTransferUtility.UploadAsync(fileTouplaod, bucketName, FileStreamUpload);
                }
                //option 4
                var fileTransferUtilityRequest = new TransferUtilityUploadRequest
                {
                    BucketName = bucketName,
                    FilePath = FilePath,
                    StorageClass = S3StorageClass.Standard,
                    PartSize = 6291456,
                    Key = AdvancedUpload,
                    CannedACL = S3CannedACL.NoACL
                };
                fileTransferUtilityRequest.Metadata.Add("parm1", "Value1");
                fileTransferUtilityRequest.Metadata.Add("parm2", "Value2");

                await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);

            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("error encoutered on server. Message: '{0}' when writing an object", e.Message);  
            }
            catch (Exception e)
            {
                Console.WriteLine("UnKnown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }

        public async Task GetObjectFromS3Async(string buckName)
        {
            const string keyName = "Steps.txt";

            try
            {
                var request = new GetObjectRequest
                { 
                        BucketName = buckName,
                        Key = keyName
                        
                };
                string responseBody;
                using (var response = await _client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                using (var reader = new StreamReader(responseStream))
                {
                    var title = response.Metadata["x-amz-meta-title"];
                    var contentType = response.Headers["Content-Type"];

                    responseBody = reader.ReadToEnd();
                }
                var pathAndFileName = $"D:\\{keyName}";
                var createText = responseBody;
                File.WriteAllText(pathAndFileName, createText);
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("error encoutered on server. Message: '{0}' when writing an object", e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("UnKnown encountered on server. Message:'{0}' when writing an object", e.Message);
            }
        }
    }
}
