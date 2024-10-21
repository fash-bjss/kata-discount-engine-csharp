using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class ItemDiscountDictionary
    {
        private Dictionary<string, List<string>> itemDiscountList = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> discounts = new Dictionary<string, List<string>>();
        public ItemDiscountDictionary() {
            discounts = itemDiscountList;
        }

        public void Add(string key, List<string> items) {
            itemDiscountList.Add(key, items); 
        }

    }
}
