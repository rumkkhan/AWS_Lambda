using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Util;
using S3Test.Models;

namespace S3Test.services
{
    public class ItemListService : IItemListsService
    {
        private readonly Dictionary<string, int> _itemList = new Dictionary<string, int>();
        private readonly IAmazonS3 _client;
        public ItemListService(IAmazonS3 client)
        {
            _client = client;
        }
       

        public async Task<Dictionary<string, int>> Getitems()
        {
            var bucketName = "ramzan";
            var res = await AmazonS3Util.DoesS3BucketExistV2Async(_client, bucketName);
            return   _itemList;
        }

      

        public void AddItemsToList(Item item)
        {
            _itemList.Add(item.ItemName, item.qty);
        }

        public void DeleteItems(Item item)
        {
            _itemList.Remove(item.ItemName);
        }
    }
}
