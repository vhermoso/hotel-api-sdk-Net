using System.Collections.Generic;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class BookingRoom
    {
        public string rateKey { get; set; }
        public List<Pax> paxes;
    }
}
