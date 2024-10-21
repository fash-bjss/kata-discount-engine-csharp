using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class ItemCounter
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();
        public ItemCounter() 
        {
        }
        public void AggregateItems(List<Item> itemList)
        {

            itemList = TwoForOneDiscount.CalculateDiscount(itemList);
            itemList = BulkDiscount.CalculateDiscount(itemList);

        }
    }
}
