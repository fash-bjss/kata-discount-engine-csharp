using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class Discounter : IDiscount
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();
        public Discounter() 
        {
        }
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemListDiscounts)
        {

            TwoForOneDiscount.CalculateDiscount(itemList, itemListDiscounts);
            BulkDiscount.CalculateDiscount(itemList, itemListDiscounts);

        }

        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }

        public List<string> GetDiscountList()
        {
            throw new NotImplementedException();
        }


    }
}
