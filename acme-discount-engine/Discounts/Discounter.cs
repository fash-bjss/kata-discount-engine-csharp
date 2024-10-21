using acme_discount_engine.Discounts.Interfaces;
using acme_discount_engine.Discounts.DiscountTypes;
using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class Discounter : IDiscount
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();
        IDiscount PerishableDiscount { get; set; } = new PerishableDiscount();

        ItemDiscountDictionary _itemDiscountDictionary { get; set; }
        public Discounter(ItemDiscountDictionary itemDiscountList) 
        {
            _itemDiscountDictionary = itemDiscountList;
        }
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            TwoForOneDiscount.CalculateDiscount(itemList, _itemDiscountDictionary);
            BulkDiscount.CalculateDiscount(itemList, _itemDiscountDictionary);
            PerishableDiscount.CalculateDiscount(itemList, _itemDiscountDictionary, Time);

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
