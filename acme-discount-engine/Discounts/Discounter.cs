using acme_discount_engine.Discounts.Interfaces;
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
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            TwoForOneDiscount.CalculateDiscount(itemList, itemDiscounts);
            BulkDiscount.CalculateDiscount(itemList, itemDiscounts);
            PerishableDiscount.CalculateDiscount(itemList, itemDiscounts, Time);

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
