using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class NoDiscount : IDiscount
    {
        private List<string> NoDiscountList = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };
        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary, int current)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDiscountList()
        {
            return NoDiscountList;
        }
    }
}
