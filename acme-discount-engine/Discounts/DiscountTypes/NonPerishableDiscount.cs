﻿using acme_discount_engine.Discounts.Interfaces;
using AcmeSharedModels;

namespace acme_discount_engine.Discounts.DiscountTypes
{
    internal class NonPerishableDiscount : IDiscount
    {
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts, DateTime Time)
        {
            itemList.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (Item item in itemList)
            {
                int daysUntilDate = (item.Date - DateTime.Today).Days;
                if (DateTime.Today > item.Date) { daysUntilDate = -1; }

                if (!item.IsPerishable && !itemDiscounts.discounts["NoDiscount"].Contains(item.Name))
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
        public void CalculateDiscount(List<Item> itemList, ItemDiscountDictionary itemDiscounts)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount(List<Item> itemList, DateTime Time)
        {
            throw new NotImplementedException();
        }
        public void CalculateDiscount(List<Item> itemList)
        {
            throw new NotImplementedException();
        }

        public void CalculateDiscount()
        {
            throw new NotImplementedException();
        }

    }
}
