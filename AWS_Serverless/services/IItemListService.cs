using AWS_Serverless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS_Serverless.services
{
    public interface IItemListService
    {
        Task<Dictionary<string, int>> Getitems();
        void AddItemsToList(Item item);
        void DeleteItems(Item item);
    }
}
