using acme_discount_engine.Discounts.Interfaces;
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
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts)
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

                bool isBulkDiscount = itemCountDictionary[itemList[i].Name] == itemQuantityLimit && !itemDiscounts.discounts["TwoForOne"].Contains(itemList[i].Name) && itemList[i].Price >= bulkDiscountPriceLimit;
                if (isBulkDiscount)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        itemList[i - j].Price -= itemList[i - j].Price * discountValue;
                    }
                }
            }
        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }
    }
}
