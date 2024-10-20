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
        Dictionary<string, int> _itemCountDictionary;

        private List<string> TwoForOneList = new List<string> { "Freddo" };

        public TwoForOne(Dictionary<string, int> itemCountDictionary)
        {
            _itemCountDictionary = itemCountDictionary;
        }

        public List<Item> CalculateDiscount(List<Item> itemList)
        {
            for (int i = 0; i < _itemCountDictionary.ToList().Count(); i++)
            {
                // By changing the dictionary to a list I can still access key and value
                string dictItemName = _itemCountDictionary.ToList()[i].Key;
                int dictItemAmount = _itemCountDictionary.ToList()[i].Value;

                if (isApplicableForDiscount(dictItemName))
                {
                    itemList[i].Price = 0;
                }
            }

            return itemList;
        }

        public bool isApplicableForDiscount(string itemName)
        {
            int itemAmount = _itemCountDictionary[itemName];
            return itemAmount == 3 && TwoForOneList.Contains(itemName);
        }
    }
}
