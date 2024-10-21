using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class TwoForOne : IDiscount
    {
    
        private List<string> TwoForOneList = new List<string> { "Freddo" };

        public TwoForOne()
        {
        }

        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary, int current)
        {
            int itemQuantityLimit = 3;
            int discountValue = 0;
            bool isTwoForOneDiscount = itemCountDictionary[itemList[current].Name] == itemQuantityLimit && TwoForOneList.Contains(itemList[current].Name);

            if (isTwoForOneDiscount)
            {
                itemList[current].Price = discountValue;
            }

            return itemList;
        }

        public List<string> GetDiscountList()
        {
            return TwoForOneList;
        }

    }
}
