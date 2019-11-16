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
       public async Task<IActionResult> AddFile([FromRoute] string backetName)
        {
            var response = await _service.uploadFileAsync(backetName);
            return Ok(response);
        }
    }
}