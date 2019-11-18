using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using AWS_Serverless.Models;
using AWS_Serverless.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AWS_Serverless.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IItemListService _itemListService;
        private readonly IAmazonS3 _client;
       
       
        public ToDoListController(IItemListService itemListService)
        {
           
         
            _itemListService = itemListService;
        }
        [HttpGet("{name}")]
        public IActionResult List(string name)
        {
            //var result = _itemListService.Getitems();

            return Ok(name);
        }

        //[HttpPost]
        //public IActionResult AddItems([FromBody]string name)
        //{

        //    //_itemListService.AddItemsToList(item);

        //    return Ok(name);
        //}
        //[HttpDelete]
        //public IActionResult DeleteItems([FromBody] Item item)
        //{
        //    _itemListService.DeleteItems(item);
        //    return Ok();
        //}

       
    }
}