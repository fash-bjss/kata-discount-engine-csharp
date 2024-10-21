﻿using AcmeSharedModels;

namespace acme_discount_engine.Discounts
{
    public class ItemCounter :IDiscount
    {
        IDiscount TwoForOneDiscount { get; set; }  = new TwoForOne();
        IDiscount BulkDiscount { get; set; } = new BulkDiscount();
        public ItemCounter() 
        {
        }
        public void CalculateDiscount(List<Item> itemList)
        {

            TwoForOneDiscount.CalculateDiscount(itemList);
            BulkDiscount.CalculateDiscount(itemList);

        }

        public List<string> GetDiscountList()
        {
            throw new NotImplementedException();
        }
    }
}
