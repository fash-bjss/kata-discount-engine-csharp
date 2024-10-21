using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acme_discount_engine.Discounts
{
    public class LoyaltyCardSystem
    {
        private bool _loyaltyCard;
        private double maxLimit { get; } = 50.0;
        private double loyaltyDiscountPercent { get; } = 0.02;
        public LoyaltyCardSystem(bool LoyaltyCard) {
        
            _loyaltyCard = LoyaltyCard;
        }


        public double Proccess(double totalBeforeLoyalty)
        {
            bool isEligibleForLoyalty = _loyaltyCard && totalBeforeLoyalty >= maxLimit;
            double costWithLoyalty = totalBeforeLoyalty - totalBeforeLoyalty * loyaltyDiscountPercent;

            return isEligibleForLoyalty ? costWithLoyalty : totalBeforeLoyalty;
        }
    }
}
