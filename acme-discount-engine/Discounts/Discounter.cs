using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class Discounter : IDiscount
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();

        IDiscount PerishableDiscount { get; set; } = new PerishableDiscount();
        public Discounter() 
        {
        }
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemListDiscounts, DateTime Time)
        {
            itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            TwoForOneDiscount.CalculateDiscount(itemList, itemListDiscounts);
            BulkDiscount.CalculateDiscount(itemList, itemListDiscounts);
            PerishableDiscount.CalculateDiscount(itemList, itemListDiscounts, Time);

        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }
    }
}
