using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class ItemCounter
    {
        public ItemCounter() 
        {
        }
        public Dictionary<string, int> AggregateItems(List<Item> _itemList)
        {
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            for(int i = 0;  i < _itemList.Count; i++)
            {
                if (itemCountDictionary.ContainsKey(_itemList[i].Name))
                {
                    itemCountDictionary[_itemList[i].Name]++;
                } else
                {
                    itemCountDictionary.Add(_itemList[i].Name, 1);
                }

                // Should be abstracted into another discount class
                IDiscount twoForone = new TwoForOne();
                List<string> two4oneList = twoForone.GetDiscountList();

                if (itemCountDictionary[_itemList[i].Name] == 10 && !two4oneList.Contains(_itemList[i].Name) && _itemList[i].Price >= 5.00)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        _itemList[i - j].Price -= _itemList[i - j].Price * 0.02;
                    }
                }
            }

            return itemCountDictionary;
        }
    }
}
