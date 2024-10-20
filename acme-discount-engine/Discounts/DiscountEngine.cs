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
        public List<Item> _itemList = new List<Item>();

        private List<string> TwoForOneList = new List<string> { "Freddo" };
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

        public bool isApplicableForDiscount(string itemName)
        {
            int itemAmount = _itemCountDictionary[itemName];
            return itemAmount == 3 && TwoForOneList.Contains(itemName);
        }

        private void CheckIfItemIsTwoForOne()
        {
            for (int i = 0; i < _itemCountDictionary.ToList().Count(); i++) {
                // By changing the dictionary to a list I can still access key and value
                string dictItemName = _itemCountDictionary.ToList()[i].Key;
                int dictItemAmount = _itemCountDictionary.ToList()[i].Value;

                if (isApplicableForDiscount(dictItemName)) {
                    _itemList[i].Price = 0;
                }
            }
        }

        private void doSomething()
        {
            string currentItem = string.Empty;
            int itemCount = 0;
            // There is a bug here
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

        private double IsPerishable() {
            double itemTotal = 0.00;
            foreach (var item in _itemList)
            {
                itemTotal += item.Price;
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if (DateTime.Today > item.Date) { daysUntilDate = -1; }

                if (!item.IsPerishable)
                {
                    if (!NoDiscount.Contains(item.Name))
                    {
                        if (daysUntilDate >= 6 && daysUntilDate <= 10)
                        {
                            item.Price -= item.Price * 0.05;
                        }
                        else if (daysUntilDate >= 0 && daysUntilDate <= 5)
                        {
                            item.Price -= item.Price * 0.10;
                        }
                        else if (daysUntilDate < 0)
                        {
                            item.Price -= item.Price * 0.20;
                        }
                    }
                }
                else
                {
                    if (daysUntilDate == 0)
                    {
                        if (Time.Hour >= 0 && Time.Hour < 12)
                        {
                            item.Price -= item.Price * 0.05;
                        }
                        else if (Time.Hour >= 12 && Time.Hour < 16)
                        {
                            item.Price -= item.Price * 0.10;
                        }
                        else if (Time.Hour >= 16 && Time.Hour < 18)
                        {
                            item.Price -= item.Price * 0.15;
                        }
                        else if (Time.Hour >= 18)
                        {
                            item.Price -= item.Price * (!item.Name.Contains("(Meat)") ? 0.25 : 0.15);
                        }
                    }
                }
              
            }
            return itemTotal;
        }

        private void MakeItemCountDictionary()
        {
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            foreach (Item item in _itemList)
            {
                if (itemCountDictionary.ContainsKey(item.Name))
                {
                    itemCountDictionary[item.Name]++;
                }
                else
                {
                    itemCountDictionary.Add(item.Name, 1);
                }
            }

            _itemCountDictionary = itemCountDictionary;
        }

        public double ApplyDiscounts(List<Item> items)
        {
            _itemList = items;
            _itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            MakeItemCountDictionary();

            CheckIfItemIsTwoForOne();

            double itemTotal = IsPerishable();

            // TODO: Potential Bug in doSomething()
            // This function has a potential bug, it is looping through the entire list and applying discount
            // rather than using the accumulated dictionary - will leave the bug in as not to break tests
            doSomething();

            double finalTotal = _itemList.Sum(item => item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal -= finalTotal * 0.02;
            }

            return Math.Round(finalTotal, 2);
        }
    }
}

