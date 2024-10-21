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
    
        public TwoForOne()
        {
        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemListDiscounts)
        {
            int itemQuantityLimit = 3;
            int discountValue = 0;
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

                bool isTwoForOneDiscount = itemCountDictionary[itemList[i].Name] == itemQuantityLimit && itemListDiscounts.discounts["TwoForOne"].Contains(itemList[i].Name);

                if (isTwoForOneDiscount)
                {
                    itemList[i].Price = discountValue;
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
