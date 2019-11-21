using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Amazon.S3.Util;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using GemBox.Spreadsheet;
using NPOI.HSSF.Model;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using OfficeOpenXml;
using S3Test.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.services
{
    public class S3Service  : IS3Service
    {
        private readonly IAmazonS3 _client;
        private InternalWorkbook stream;

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
            const string keyName = "ANX -1 Normal30knew.xls";

            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = buckName,
                    Key = keyName

                };
                string responseBody;
                byte[] data;
                ISheet sheet;
                var countSheets = 0;
               
                using (var response = await _client.GetObjectAsync(request))
                using (Stream responseStream = response.ResponseStream)
                using (MemoryStream memStream = new MemoryStream())
                {
                   
                    responseStream.CopyTo(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    HSSFWorkbook hssfwb = new HSSFWorkbook(memStream);
                     sheet = hssfwb.GetSheetAt(2);
                    var gendral = hssfwb.GetSheetAt(0);
                    IRow headerRowr = sheet.GetRow(7);
                    int cellCountt = headerRowr.LastCellNum;
                    countSheets = hssfwb.Count;
                    //get first sheet from workbook  
                    for (int i = 0; i < hssfwb.Count; i++)
                    {
                        sheet = hssfwb.GetSheetAt(i + 1);
                        var Name = hssfwb.GetSheetName(i);//get sheet names
                        IRow headerRow = sheet.GetRow(5); //Get Header Row
                        int cellCount = headerRow.LastCellNum;

                     var    abc = renderData(headerRow, cellCount, sheet);
                    }



                }

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

        long count = 0;

        public long renderData(IRow headerRow, int cellCount, ISheet sheet)
        {
            for (int j = 0; j < cellCount; j++)
            {
                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                var cel = cell.ToString();
                count++;
            }
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                IRow row = sheet.GetRow(i);
                if (row == null) continue;
                if (row.Cells.All(d => d.CellType == NPOI.SS.UserModel.CellType.Blank)) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        var c = row.GetCell(j).ToString();
                    }
                    count++;
                }
            }
            return count;
        }
    }
}


//Spire.Xls.Workbook workbook = new Spire.Xls.Workbook();
//workbook.LoadFromStream(memStream);
//var sheett = workbook.Worksheets.Count;
//var row = workbook.Worksheets[2];
//var rows = workbook.Worksheets[1];
//var rowss = workbook.Worksheets[3];
//var r = row.SelectionCount;
//var dd = row.GetRowHeight(2);
// var atr =      row.Range["A6"].Value;
//var atr2 = row.Range["A7"].Value;
