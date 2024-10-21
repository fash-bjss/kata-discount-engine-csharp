using AcmeSharedModels;
using System.Collections.Generic;
using System.Data;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        private ItemDiscountDictionary itemListDiscounts { get; set; } = new ItemDiscountDictionary();

        public DiscountEngine() 
        {
            itemListDiscounts.Add("TwoForOne", ["Freddo"]);
            itemListDiscounts.Add("NoDiscount", ["T-Shirt", "Keyboard", "Drill", "Chair"]);
        }

        private double GetTotalPrice(List<Item> itemList)
        {
            double itemTotal = itemList.Sum(item => item.Price);
            return itemTotal;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            Discounter itemDiscounter = new Discounter(itemListDiscounts);
            itemDiscounter.CalculateDiscount(items, itemListDiscounts, Time);

            double total = GetTotalPrice(items);
            double finalTotal = new LoyaltyCardSystem(LoyaltyCard).Proccess(total);

            return Math.Round(finalTotal, 2);
        }
    }
}

