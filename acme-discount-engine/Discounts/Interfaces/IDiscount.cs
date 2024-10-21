using AcmeSharedModels;

namespace acme_discount_engine.Discounts.Interfaces
{
    public interface IDiscount
    {
        public void CalculateDiscount(List<Item> itemList);
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts);
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time);


    }
}
