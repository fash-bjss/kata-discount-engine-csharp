﻿using AcmeSharedModels;
using System.Collections.Generic;
using System.Data;

namespace acme_discount_engine.Discounts
{
    public class DiscountEngine
    {
        public bool LoyaltyCard { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;

        private Dictionary<string, int> _itemCountDictionary = new Dictionary<string, int>();

        private List<string> TwoForOneList = new List<string> { "Freddo" };
        private List<string> NoDiscount = new List<string> { "T-Shirt", "Keyboard", "Drill", "Chair" };

        public bool isApplicableForDiscount(string itemName)
        {
            int itemAmount = _itemCountDictionary[itemName];
            return itemAmount == 3 && TwoForOneList.Contains(itemName);
        }

        public void CheckIfItemIsTwoForOne(List<Item> items)
        {
            for (int i = 0; i < _itemCountDictionary.ToList().Count(); i++) {
                // By changing the dictionary to a list I can still access key and value
                string dictItemName = _itemCountDictionary.ToList()[i].Key;
                int dictItemAmount = _itemCountDictionary.ToList()[i].Value;

                if (isApplicableForDiscount(dictItemName)) {
                    items[i].Price = 0;
                }
            }
        }

        public double IsPerishable(List<Item> items) {
            double itemTotal = 0.00;
            foreach (var item in items)
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

        private void MakeItemCountDictionary(List<Item> items)
        {
            Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>();

            foreach (Item item in items)
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
            items.Sort((x, y) => x.Name.CompareTo(y.Name));

            MakeItemCountDictionary(items);

            CheckIfItemIsTwoForOne(items);

            double itemTotal = IsPerishable(items);
            
            string currentItem = string.Empty;
            int itemCount = 0;
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Name != currentItem)
                {
                    currentItem = items[i].Name;
                    itemCount = 1;
                }
                else
                {
                    itemCount++;
                    if (itemCount == 10 && !TwoForOneList.Contains(items[i].Name) && items[i].Price >= 5.00)
                    {
                        for (int j = 0; j < 10; j++)
                        {
                            items[i - j].Price -= items[i - j].Price * 0.02;
                        }
                        itemCount = 0;
                    }
                }
            }

            double finalTotal = items.Sum(item => item.Price);

            if (LoyaltyCard && itemTotal >= 50.00)
            {
                finalTotal -= finalTotal * 0.02;
            }

            return Math.Round(finalTotal, 2);
        }
    }
}

