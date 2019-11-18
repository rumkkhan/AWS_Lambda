using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3Test.Models;
using S3Test.services;

namespace S3Test.controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IItemListsService _itemListService;
        private readonly IAmazonS3 _client;


        public ToDoController(IItemListsService itemListService)
        {


            _itemListService = itemListService;
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var result = await _itemListService.Getitems();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddItems([FromBody] Item item)
        {

            _itemListService.AddItemsToList(item);

            return Ok();
        }
        [HttpDelete]
        public IActionResult DeleteItems([FromBody] Item item)
        {
            _itemListService.DeleteItems(item);
            return Ok();
        }

    }
}