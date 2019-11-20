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
        public async Task<DataTable> FunctionHandler(S3Event evnt, ILambdaContext context)
        {
            var s3Event = evnt.Records?[0].S3;
            if (s3Event == null)
            {
                return null;
            }

            try
            {
                //if (s3Event.Object.Key.ToLower().Contains("thumb"))
                //{
                //    //Console.WriteLine("The image is already a thumb file");
                //    return "The file is aready a thumb image file";
                //}

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
                string responseBody;
                byte[] data;
                using (var response = await client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)

                using (XLWorkbook workBook = new XLWorkbook(responseStream))
                {
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    //Create a new DataTable.
                    DataTable dt = new DataTable();
                    bool firstRow = true;

                    foreach (IXLRow row in workSheet.Rows())
                    {
                        //Use the first row to add columns to DataTable.
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                Console.WriteLine(cell.Value.ToString());
                                dt.Columns.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            //Add rows to DataTable.
                            dt.Rows.Add();
                            int i = 0;
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                i++;
                            }
                        }

                    }
                    //    using (var reader = new StreamReader(responseStream))
                    //{
                    //    var title = response.Metadata["x-amz-meta-title"];
                    //    var contentType = response.Headers["Content-Type"];
                    //    var pathAndFileName = $"D:\\{keyName}";
                    //    responseBody = reader.ReadToEnd();
                    //    using (MemoryStream ms = new MemoryStream())
                    //    {
                    //        reader.BaseStream.CopyTo(ms);
                    //        data = ms.ToArray();
                    //    }
                    //    File.WriteAllBytes("pathAndFileName", data);
                    //    //byte[] bytes = reader;
                    //}
                    Console.WriteLine("No of rows",dt.Rows.Count);
                    return dt;
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
