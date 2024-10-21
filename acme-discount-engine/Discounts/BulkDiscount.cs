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
        // Keep two for one list here for the time being
        List<string> TwoForOneList { get; set; } = new TwoForOne().GetDiscountList();
        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary, int current)
        {
            int itemQuantityLimit = 10;
            double bulkDiscountPriceLimit = 5.00;
            double discountValue = 0.02;
            bool isBulkDiscount = itemCountDictionary[itemList[current].Name] == itemQuantityLimit && !TwoForOneList.Contains(itemList[current].Name) && itemList[current].Price >= bulkDiscountPriceLimit;
            if (isBulkDiscount)
            {
                for (int next = 0; next < 10; next++)
                {
                    itemList[current - next].Price -= itemList[current - next].Price * discountValue;
                }
            }
            return itemList;
        }

        public List<string> GetDiscountList()
        {
            throw new NotImplementedException();
        }
    }
}
