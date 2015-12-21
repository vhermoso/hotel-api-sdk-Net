using System;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class ShiftRate : BasicRate
    {
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
    }
}
