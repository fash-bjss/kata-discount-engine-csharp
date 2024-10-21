using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class BulkDiscount : IDiscount
    {
        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary, int current)
        {

            return itemList;
        }

        public List<string> GetDiscountList()
        {
            throw new NotImplementedException();
        }
    }
}
