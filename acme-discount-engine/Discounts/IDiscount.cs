using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public interface IDiscount
    {
        public List<Item> CalculateDiscount(List<Item> itemList, Dictionary<string, int> itemCountDictionary);

        public List<string> GetDiscountList();

    }
}
