using System;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class CancellationPolicy
    {
        public decimal amount { get; set; }
        public decimal hotelAmount { get; set; }
        public string hotelCurrency { get; set; }
        public string from { get; set; }
    }
}
