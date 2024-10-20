using AcmeSharedModels;
using System.Collections.Generic;
using System.Data;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private Dictionary<string, int> _itemCountDictionary = new Dictionary<string, int>();
        private List<Item> _itemList = new List<Item>();
        private ItemCounter itemCounter = new ItemCounter();

        IDiscount twoForOneDiscount = new TwoForOne();
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };


        // TODO: Potential bug in this doSomething function
        private void BuggyDiscountFunction()
        {
            string currentItem = string.Empty;
            int itemCount = 0;
            List<string> TwoForOneList = twoForOneDiscount.GetDiscountList();

            for (int i = 0; i < _itemList.Count(); i++)
            {
                if (_itemList[i].Name != currentItem)
                {
                    currentItem = _itemList[i].Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;

                }

                if (itemCount == 10 && !TwoForOneList.Contains(_itemList[i].Name) && _itemList[i].Price >= 5.00)
                {
                    for (int j = 0; j < 10; j++)
                    {
                        _itemList[i - j].Price -= _itemList[i - j].Price * 0.02;
                    }
                    itemCount = 0;
                }
            }
        }

        private double GetTotalPrice()
        {
            double itemTotal = _itemList.Sum(item => item.Price);
            return itemTotal;
        }
        public int SetDaysUntil(Item item)
        {
            int daysUntilDate = (item.Date - DateTime.Today).Days;
            if (DateTime.Today > item.Date) { daysUntilDate = -1; }
            return daysUntilDate;
        }
        private void IsPerishable() {

            foreach (Item item in _itemList)
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

                if (!item.IsPerishable && !NoDiscount.Contains(item.Name))
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

        public double ApplyDiscounts(List<Item> items)
        {
            _itemList = items;
            _itemList.Sort((x, y) => x.Name.CompareTo(y.Name));
            _itemCountDictionary = itemCounter.AggregateItems(_itemList);

            _itemList = twoForOneDiscount.CalculateDiscount(_itemList, _itemCountDictionary);

            IsPerishable();

            // TODO: Potential Bug in doSomething()
            // This function has a potential bug, it is looping through the entire list and applying discount
            // rather than using the accumulated dictionary - will leave the bug in as not to break tests
            BuggyDiscountFunction();

            double total = GetTotalPrice();
            double finalTotal = LoyaltyDiscountProcess(total);

            return Math.Round(finalTotal, 2);
        }
    }
}

