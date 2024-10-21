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

            // Two for one discount
            int twoForOne = 3;
            bool isTwoForOneDiscount = itemCountDictionary[itemList[current].Name] == twoForOne && TwoForOneList.Contains(itemList[current].Name);

            if (isTwoForOneDiscount)
            {
                itemList[current].Price = 0;
            }

            // Is bulk discount
            double bulDiscountPriceLimit = 5.00;
            int bulDiscountQuantityLimit = 10;
            bool isBulkDiscount = itemCountDictionary[itemList[current].Name] == bulDiscountQuantityLimit && !TwoForOneList.Contains(itemList[current].Name) && itemList[current].Price >= bulDiscountPriceLimit;
            if (isBulkDiscount)
            {
                for (int next = 0; next < 10; next++)
                {
                    itemList[current - next].Price -= itemList[current - next].Price * 0.02;
                }
            }

            return itemList;
        }

        public List<string> GetDiscountList()
        {
            return TwoForOneList;
        }

    }
}
