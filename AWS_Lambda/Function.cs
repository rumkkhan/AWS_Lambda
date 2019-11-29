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
using ClosedXML.Excel;
using ExcelDataReader;
using NPOI.HSSF.Record;
using NPOI.HSSF.UserModel;
using NPOI.POIFS.FileSystem;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

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
       
        public async Task<string> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if (s3Event == null)
            {
                return null;
            }

            try
            {

                string filename = s3Event.Object.Key;

                string extension = Path.GetExtension(filename);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filename);
                AmazonS3Client client = new AmazonS3Client(Amazon.RegionEndpoint.APSouth1);

            

                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = s3Event.Bucket.Name,
                    Key = s3Event.Object.Key
                };
                Console.WriteLine($"bucket name:{s3Event.Bucket.Name}");
                Console.WriteLine($"Key:{s3Event.Object.Key}");

               
                ISheet sheet;
                var countSheets = 0;
                using (var response = await client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (MemoryStream memStream = new MemoryStream())
                {
                    //var title = response.Metadata["x-amz-meta-anxid"];

                    Console.WriteLine("working....");

                    if (extension == ".xls")
                    {
                        responseStream.CopyTo(memStream);
                        memStream.Seek(0, SeekOrigin.Begin);

                        Console.WriteLine($"working...11.{memStream}");
                        HSSFWorkbook hssfwb = new HSSFWorkbook(memStream);
                        Console.WriteLine("working...1222.");
                        sheet = hssfwb.GetSheetAt(2);
                        Console.WriteLine("working...12.");
                        var gendral = hssfwb.GetSheetAt(0);
                        IRow headerRowr = sheet.GetRow(7);
                        int cellCountt = headerRowr.LastCellNum;
                        countSheets = hssfwb.Count;
                        Console.WriteLine("working....", countSheets);
                        //get first sheet from workbook  
                        //for (int i = 0; i < hssfwb.Count; i++)
                        //{
                        sheet = hssfwb.GetSheetAt(2);
                        Console.WriteLine("success");

                        var Name = hssfwb.GetSheetName(2);//get sheet names
                        IRow headerRow = sheet.GetRow(5); //Get Header Row
                        int cellCount = headerRow.LastCellNum;
                    }
                    else
                    {
                        using (var responsee = await client.GetObjectAsync(request))
                        using (Stream responseStreame = response.ResponseStream)
                        using (MemoryStream memStreame = new MemoryStream())
                        {
                            responseStreame.CopyTo(memStreame);
                            memStreame.Seek(0, SeekOrigin.Begin);
                            XSSFWorkbook hssfwb = new XSSFWorkbook(memStreame); //This will read 2007 Excel format  
                            Console.WriteLine("working...1222.");
                            sheet = hssfwb.GetSheetAt(2);
                            Console.WriteLine("working...12.");
                            var gendral = hssfwb.GetSheetAt(0);
                            IRow headerRowr = sheet.GetRow(7);
                            int cellCountt = headerRowr.LastCellNum;
                            countSheets = hssfwb.Count;
                            Console.WriteLine($"working....:{countSheets}" );
                            //get first sheet from workbook  
                            //for (int i = 0; i < hssfwb.Count; i++)
                            //{
                            sheet = hssfwb.GetSheetAt(2);
                            Console.WriteLine("working....");

                            var Name = hssfwb.GetSheetName(2);//get sheet names
                            IRow headerRow = sheet.GetRow(5); //Get Header Row
                            int cellCount = headerRow.LastCellNum;



                            Console.WriteLine("working.... done");
                            
                          
                        }
                     
                    }
                    return null;
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
