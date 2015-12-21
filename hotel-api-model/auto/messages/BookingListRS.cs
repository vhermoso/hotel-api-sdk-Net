using com.hotelbeds.distribution.hotel_api_model.auto.model;

namespace com.hotelbeds.distribution.hotel_api_model.auto.messages
{
    public class BookingListRS : AbstractGenericResponse
    {
        public Bookings bookings { get; set; }
    }
}
