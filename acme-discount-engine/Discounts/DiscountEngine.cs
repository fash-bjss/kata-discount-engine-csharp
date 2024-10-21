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


        public double LoyaltyDiscountProcess(double totalBeforeLoyalty)
        {
            double maxLimit = 50.0;
            double loyaltyDiscountPercent = 0.02;

            bool isEligibleForLoyalty = LoyaltyCard && totalBeforeLoyalty >= maxLimit;
            double costWithLoyalty = totalBeforeLoyalty - totalBeforeLoyalty * loyaltyDiscountPercent;

            return isEligibleForLoyalty ? costWithLoyalty : totalBeforeLoyalty;
        }

        private double GetTotalPrice()
        {
            double itemTotal = itemList.Sum(item => item.Price);
            return itemTotal;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            itemListDiscounts.Add("TwoForOne", ["Freddo"]);
            itemListDiscounts.Add("NoDiscount", ["T-Shirt", "Keyboard", "Drill", "Chair"]);

            itemList = items;
            itemDiscounter.CalculateDiscount(itemList, itemListDiscounts, Time);

            double total = GetTotalPrice();
            double finalTotal = LoyaltyDiscountProcess(total);

            return Math.Round(finalTotal, 2);
        }
    }
}

