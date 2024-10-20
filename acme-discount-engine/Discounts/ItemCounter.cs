using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class ItemCounter
    {
        public ItemCounter() 
        {
        }

        public Dictionary<string, int> SumAllItems(List<Item> _itemList)
        {
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            foreach (Item item in _itemList)
            {
                if (itemCountDictionary.ContainsKey(item.Name))
                {
                    itemCountDictionary[item.Name]++;
                }
                else
                {
                    itemCountDictionary.Add(item.Name, 1);
                }
            }

            return itemCountDictionary;
        }
    }
}
