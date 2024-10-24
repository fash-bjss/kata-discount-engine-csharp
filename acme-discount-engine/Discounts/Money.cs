using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class Money
    {
        private double _value {  get; }
        public Money(double value) {
            _value = value;
        }
    }
}
