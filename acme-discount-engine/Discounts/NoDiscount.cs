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
        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts)
        {
            throw new NotImplementedException();
        }


        public List<string> GetDiscountList()
        {
            return NoDiscountList;
        }
    }
}
