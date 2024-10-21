using AcmeSharedModels;
using System.Collections.Generic;
using System.Data;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();
        private List<Item> itemList = new List<Item>();
        private Discounter itemDiscounter = new Discounter();
        public ItemDiscountDictionary itemListDiscounts = new ItemDiscountDictionary();

        private double GetTotalPrice()
        {
            double itemTotal = itemList.Sum(item => item.Price);
            return itemTotal;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            itemList = items;
            itemListDiscounts.Add("TwoForOne", ["Freddo"]);
            itemListDiscounts.Add("NoDiscount", ["T-Shirt", "Keyboard", "Drill", "Chair"]);

            itemDiscounter.CalculateDiscount(itemList, itemListDiscounts, Time);

            double total = GetTotalPrice();
            double finalTotal = new LoyaltyCardSystem(LoyaltyCard).Proccess(total);

            return Math.Round(finalTotal, 2);
        }
    }
}

