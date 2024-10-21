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
        public void CalculateDiscount(List<Item> itemList)
        {
            int itemQuantityLimit = 10;
            double discountValue = 0.02;
            double bulkDiscountPriceLimit = 5.00;
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            for (int i = 0; i < itemList.Count; i++)
            {
                if (itemCountDictionary.ContainsKey(itemList[i].Name))
                {
                    itemCountDictionary[itemList[i].Name]++;
                }
                else
                {
                    itemCountDictionary.Add(itemList[i].Name, 1);
                }

                bool isBulkDiscount = itemCountDictionary[itemList[i].Name] == itemQuantityLimit && !TwoForOneList.Contains(itemList[i].Name) && itemList[i].Price >= bulkDiscountPriceLimit;
                if (isBulkDiscount)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        itemList[i - j].Price -= itemList[i - j].Price * discountValue;
                    }
                }
            }
        }

        public List<string> GetDiscountList()
        {
            throw new NotImplementedException();
        }
    }
}
