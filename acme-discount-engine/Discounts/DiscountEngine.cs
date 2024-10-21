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
        public ItemDiscountDictionary itemListDiscounts = new ItemDiscountDictionary();
        private Discounter itemDiscounter = new Discounter();

        public int SetDaysUntil(Item item)
        {
            int daysUntilDate = (item.Date - DateTime.Today).Days;
            if (DateTime.Today > item.Date) { daysUntilDate = -1; }
            return daysUntilDate;
        }
        private void IsPerishable() {

            foreach (Item item in itemList)
            {
                int daysUntilDate = SetDaysUntil(item);

                if (item.IsPerishable && daysUntilDate == 0)
                {
                    if (Time.Hour > 17)
                    {
                        item.Price -= item.Price * (!item.Name.Contains("(Meat)") ? 0.25 : 0.15);
                    }
                    else if (Time.Hour > 15)
                    {
                        item.Price -= item.Price * 0.15;
                    }
                    else if (Time.Hour > 11)
                    {
                        item.Price -= item.Price * 0.10;
                    }
                    else
                    {
                        item.Price -= item.Price * 0.05;
                    }
                }

                if (!item.IsPerishable && !itemListDiscounts.discounts["NoDiscount"].Contains(item.Name))
                {

                    if (daysUntilDate < 0)
                    {
                        item.Price -= item.Price * 0.20;
                    }

                    else if (daysUntilDate < 6)
                    {
                        item.Price -= item.Price * 0.10;
                    }

                    else if (daysUntilDate < 11)
                    {
                        item.Price -= item.Price * 0.05;
                    }

                }

            }
        }
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
            itemList.Sort((x, y) => x.Name.CompareTo(y.Name));
            itemDiscounter.CalculateDiscount(itemList, itemListDiscounts);

            IsPerishable();

            double total = GetTotalPrice();
            double finalTotal = LoyaltyDiscountProcess(total);

            return Math.Round(finalTotal, 2);
        }
    }
}

