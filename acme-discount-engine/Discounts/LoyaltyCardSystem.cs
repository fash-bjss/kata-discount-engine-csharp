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
        public LoyaltyCardSystem(bool LoyaltyCard) {
        
            _loyaltyCard = LoyaltyCard;
        }


        public double Proccess(double totalBeforeLoyalty)
        {
            double maxLimit = 50.0;
            double loyaltyDiscountPercent = 0.02;

            bool isEligibleForLoyalty = _loyaltyCard && totalBeforeLoyalty >= maxLimit;
            double costWithLoyalty = totalBeforeLoyalty - totalBeforeLoyalty * loyaltyDiscountPercent;

            return isEligibleForLoyalty ? costWithLoyalty : totalBeforeLoyalty;
        }
    }
}
