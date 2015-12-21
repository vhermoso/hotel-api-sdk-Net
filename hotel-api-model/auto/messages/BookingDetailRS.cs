using com.hotelbeds.distribution.hotel_api_model.auto.model;

namespace com.hotelbeds.distribution.hotel_api_model.auto.messages
{
    public class BookingDetailRS : AbstractGenericResponse
    {
        public Booking booking { get; set; }
    }
}
