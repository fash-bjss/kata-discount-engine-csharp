using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class ItemCounter
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        public ItemCounter() 
        {
        }
        public Dictionary<string, int> AggregateItems(List<Item> itemList)
        {
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            for(int i = 0;  i < itemList.Count; i++)
            {
                if (itemCountDictionary.ContainsKey(itemList[i].Name))
                {
                    itemCountDictionary[itemList[i].Name]++;
                } else
                {
                    itemCountDictionary.Add(itemList[i].Name, 1);
                }
                itemList = TwoForOneDiscount.CalculateDiscount(itemList, itemCountDictionary, i);

            }

            return itemCountDictionary;
        }
    }
}
