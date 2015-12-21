using System.Collections.Generic;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class RateBreakDown
    {
        public List<RateDiscount> rateDiscounts { get; set; }
        public List<RateSupplement> rateSupplements { get; set; }
    }
}
