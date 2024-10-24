using acme_discount_engine.Discounts.Interfaces;
using acme_discount_engine.Discounts.DiscountTypes;
using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class Discounter : IDiscount
    {
        IDiscount TwoForOneDiscount { get; set; } = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();
        IDiscount PerishableDiscount { get; set; } = new PerishableDiscount();
        IDiscount NonPerishableDiscount { get; set; } = new NonPerishableDiscount();

        private ItemDiscountDictionary _itemDiscountDictionary { get; set; }
        private List<Item> _itemList {get; set;}
        private DateTime _time { get; set; }
        public Discounter(ItemDiscountDictionary itemDiscountList, DateTime time, List<Item> itemList) 
        {
            _itemDiscountDictionary = itemDiscountList;
            _time = time;
            _itemList = itemList;

        }
        public void CalculateDiscount()
        {
            _itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            TwoForOneDiscount.CalculateDiscount(_itemList, _itemDiscountDictionary);
            BulkDiscount.CalculateDiscount(_itemList, _itemDiscountDictionary);
            PerishableDiscount.CalculateDiscount(_itemList, _itemDiscountDictionary, _time);
            NonPerishableDiscount.CalculateDiscount(_itemList, _itemDiscountDictionary, _time);
        }
        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList, DateTime Time)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            throw new NotImplementedException();
        }


    }
}
