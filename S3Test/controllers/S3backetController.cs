using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3Test.services;

namespace S3Test.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class S3backetController : ControllerBase
    {
        private readonly IS3Service _service;

        public S3backetController(IS3Service service)
        {
            _service = service;
        }

        [HttpPost("{bucketName}")]
        public async Task<IActionResult> CreateBucket([FromRoute] string bucketName)
        {
            var response = await _service.CreateBucketAsync(bucketName);

            return Ok(response);
        }

       [HttpPost]
       [Route("AddFile/{bucketName}")]
       public async Task<IActionResult> AddFile([FromRoute] string bucketName)
        {
             await _service.UploadFileSync(bucketName);
            return Ok();
        }

        [HttpGet]
        [Route("GetFile/{bucketName}")]
        public async Task<IActionResult> GetObjectFromS3Async([FromRoute] string bucketName)
        {
           var result =  await  _service.GetObjectFromS3Async(bucketName);

            return Ok(result);
        }
    }
}