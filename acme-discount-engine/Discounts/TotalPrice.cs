using AcmeSharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class TotalPrice
    {
        public double totalPrice { get; private set; }
        private List<Item> _itemList { get; set; }
        public TotalPrice(List<Item> itemList) {
            _itemList = itemList;
            totalPrice = GetTotalPrice();
        }
        private double GetTotalPrice()
        {
            double itemTotal = _itemList.Sum(item => item.Price);
            return itemTotal;
        }
    }
}
