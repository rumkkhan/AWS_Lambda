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
using NPOI.XSSF.UserModel;
using CellType = NPOI.SS.UserModel.CellType;
using System.Collections;

namespace S3Test.services
{
    public class S3Service  : IS3Service
    {
        private readonly IAmazonS3 _client;
        private InternalWorkbook stream;
        phoenixgstContext _context = new phoenixgstContext();

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

        private const string FilePath = "C:\\ANX -1 Normal30knew.xls";
        private const string UploadWithKeyName = "UploadWithKeyName";
        private const string FileStreamUpload = "FileStreamUpload";
        private const string AdvancedUpload = "excelfile";

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
                    CannedACL = S3CannedACL.NoACL,                   
                };
                fileTransferUtilityRequest.Metadata.Add("AnxID", "1234");
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

        public async Task<List<Trananx>> GetObjectFromS3Async(string buckName)
        {
            var newdata = new List<Trananx>() ;
            const string keyName = "excelfile";
            
            Assessee assessee = new Assessee();
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
                    var title = response.Metadata["x-amz-meta-anxid"];
                  
                    

                                        responseStream.CopyTo(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    HSSFWorkbook hssfwb = new HSSFWorkbook(memStream);
                     sheet = hssfwb.GetSheetAt(2);
                    var gendral = hssfwb.GetSheetAt(0);
                    IRow headerRowr = sheet.GetRow(7);
                    int cellCountt = headerRowr.LastCellNum;
                    countSheets = hssfwb.Count;
                    //get first sheet from workbook  
                    //for (int i = 0; i < hssfwb.Count; i++)
                    //{
                        sheet = hssfwb.GetSheetAt(2);
                        var Name = hssfwb.GetSheetName(2);//get sheet names
                        IRow headerRow = sheet.GetRow(5); //Get Header Row
                        int cellCount = headerRow.LastCellNum;

                       newdata = await renderData(headerRow, cellCount, sheet);
                        
                   // }


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
            var result = _context.Assessee.Where(a => a.AssesseeId == 1).FirstOrDefault();
            return newdata;
        }

        string data = "";

        public async Task<List<Trananx>> renderData(IRow headerRow, int cellCount, ISheet sheet)
        {
            List<TranAnxDto> tranAnxDtos = new List<TranAnxDto>();
            for (int j = 0; j < cellCount; j++)
            {
                NPOI.SS.UserModel.ICell cell = headerRow.GetCell(j);
                if (cell == null || string.IsNullOrWhiteSpace(cell.ToString())) continue;
                data += cell.ToString();
               
            }
            List<string[]> allData = new List<string[]>();
            List<TranAnxDto> tranAnx = new List<TranAnxDto>();
            string[] a;
            ArrayList myList = new ArrayList();
            for (int i = (sheet.FirstRowNum + 7); i <= sheet.LastRowNum; i++) //Read Excel File
            {
                IRow row = sheet.GetRow(i);
                

                a = new string[17];
                if (row == null) continue;
                if (row.Cells.All(d => d.CellType == CellType.Blank)) continue;
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        data =  row.GetCell(j).ToString();
                        a[j] = row.GetCell(j).ToString();
                    }
                    
                } 

                tranAnx.Add(new TranAnxDto
                { 
                    OrgGstin = a[0],
                    PartyName = a[1],
                    Typee = a[2],
                    InNumber = a[3],
                    Date = a[4],
                    DocValue =  decimal.Parse(a[5]),
                    TaxableValue = decimal.Parse(a[6]),
                    Rate = decimal.Parse(a[7]),
                    Igst = decimal.Parse(a[8]),
                    Cgst = decimal.Parse(a[9]),
                    Sgst = decimal.Parse(a[10]),
                    Cess = decimal.Parse(a[11]),

                    Pos = a[12],
                    HSN = a[13],
                    Diff = a[14],
                    Supply = a[15],
                }); ;
             
                myList.Add(a);
            }
            
            List<Trananx> trananx = new List<Trananx>();
            tranAnx = tranAnx.GroupBy(x => new { x.OrgGstin, x.PartyName })
                    .Select(x => new TranAnxDto
                    {
                        
                         MonthId = 201911,
                         OrgGstin = x.FirstOrDefault().OrgGstin,
                         Branch = x.FirstOrDefault().Branch,
                         Typee = x.FirstOrDefault().Typee,
                         InNumber = x.FirstOrDefault().InNumber,
                         Date = x.FirstOrDefault().Date,
                         DocValue = x.FirstOrDefault().DocValue,
                         TranId = 1,
                         PartyName = x.FirstOrDefault().PartyName,


                        Trananxdet = x.Select((y, index) => new Trananxdet
                        {
                             AnxId = 1,
                             Cess = y.Cess,
                             Hsnsac =  y.HSN,
                             Cgst = y.Cgst
      
                                  
                        }).ToList()
                    }).ToList();

            //var monthId = _context.Monthmain.Where(m => m.MonthId == 1).Select(y => y.mo;

            //period(excel cloumn) + clientId ExcelName +   GSTRNO uI +
            var tranId = _context.Monthmain.Where(m => m.TranId == 8).FirstOrDefault();

          //  var partyMaster = _context.Partymaster.Where

            //excelperio
            foreach (var item in tranAnx)
            {
                //_currTranDetails.PartyID = Common.ConvertToInt(_uiPartyNameMat.PartyDetailList.Where(c => c.GSTIN == item.ctin).Select(c => c.PartyID).FirstOrDefault());
                // if ( p === 0)
                //{
                //    PartyDetails info = new PartyDetails
                //    {
                //        PartyType = istdstcsParty ? (int)PartyType.TaxDeductor : (int)PartyType.B2B,
                //        GSTIN = item.ctin,
                //        Name = item.ctin,
                //        State = item.ctin.Trim().Remove(2)// header.pos
                //    };
                //    _uiPartyNameMat.Save(info);
                //    _currTranDetails.PartyID = Common.ConvertToInt(_uiPartyNameMat.PartyDetailList.Where(c => c.GSTIN == item.ctin).Select(c => c.PartyID).FirstOrDefault());
                //    //}
                    trananx.Add(new Trananx
                {
                   MonthId = item.MonthId,//month main
                    TranId = 8,//month main
                   OrgGstin = item.OrgGstin,
                   Branch = item.Branch,
                   ShippingNum = item.InNumber,
                   Trananxdet = item.Trananxdet 
                   //PartyId = 

            });              
            } 
           
            try
            {
               await   _context.Trananx.AddRangeAsync(trananx);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {

                throw;
            }
            return trananx;
            //  return tranAnx;
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
