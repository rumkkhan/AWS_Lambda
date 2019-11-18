using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Amazon.Lambda.Core;
using Amazon.Lambda.S3Events;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using Amazon.S3;
using Amazon.S3.Model;
using ExcelDataReader;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace AWS_Lambda
{
    public class Function
    {

        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        /// 
       
            [assembly: Amazon.Lambda.Core.LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
        public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if (s3Event == null)
            {
                return null;
            }

            try
            {
                if (s3Event.Object.Key.ToLower().Contains("thumb"))
                {
                    //Console.WriteLine("The image is already a thumb file");
                    return "The file is aready a thumb image file";
                }

                string filename = s3Event.Object.Key;

                string extension = Path.GetExtension(filename); //.jpeg with "dot"
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
                AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.APSouth1);

                //using (var objectResponse = await client.GetObjectAsync(s3Event.Bucket.Name, s3Event.Object.Key))
                //{
                //    using (Stream responseStream = objectResponse.ResponseStream)
                //    {


                //            string contents = responseStream.EndRead()


                //    }
                //}

                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key
                };
                using (GetObjectResponse response = await client.GetObjectAsync(request))
                {

                    IExcelDataReader reader = ExcelReaderFactory.CreateBinaryReader(response.ResponseStream);
                    var ss = reader.GetData(1);
                    //using (IExcelDataReader reader = new ExcelReaderFactory.CreateBinaryReader(response.ResponseStream))
                    //{

                       Console.WriteLine(ss);

                    //}

                    return "Thumbnail version of the image has been created";

                }
            }
            catch (Exception e)
            {

                context.Logger.LogLine($"Error getting object {s3Event.Object.Key} from bucket {s3Event.Bucket.Name}. Make sure they exist and your bucket is in the same region as this function.");
                context.Logger.LogLine(e.Message);
                context.Logger.LogLine(e.StackTrace);
                throw;
            }
        }
    }
    }
