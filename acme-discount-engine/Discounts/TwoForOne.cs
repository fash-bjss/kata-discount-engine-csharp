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

        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary)
        {
            for (int i = 0; i < itemCountDictionary.ToList().Count(); i++)
            {
                // By changing the dictionary to a list I can still access key and value
                string dictItemName = itemCountDictionary.ToList()[i].Key;
                int dictItemAmount = itemCountDictionary.ToList()[i].Value;

                bool isApplicableForDiscount = dictItemAmount == 3 && TwoForOneList.Contains(dictItemName);

                if (isApplicableForDiscount)
                {
                    itemList[i].Price = 0;
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
