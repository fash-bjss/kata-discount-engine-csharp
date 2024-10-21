using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public interface IDiscount
    {
        public void CalculateDiscount(List<Item> itemList);

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts);


    }
}
