using S3Test.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3Test.services
{
    public interface IItemListsService
    {
       
            Task<Dictionary<string, int>> Getitems();
            void AddItemsToList(Item item);
            void DeleteItems(Item item);

    }
}
