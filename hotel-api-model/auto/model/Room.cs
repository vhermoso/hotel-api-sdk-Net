using System.Collections.Generic;
using com.hotelbeds.distribution.hotel_api_model.auto.common;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Room
    {        
        public SimpleTypes.BookingStatus status;
        public string code { get; set; }
        public List<Pax> paxes { get; set; }
        public List<Rate> rates { get; set; }
    } 
}
