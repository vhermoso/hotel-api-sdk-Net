using System.Collections.Generic;
using com.hotelbeds.distribution.hotel_api_model.auto.model;

namespace com.hotelbeds.distribution.hotel_api_model.auto.messages
{
    public class AvailabilityRS : AbstractGenericResponse
    {
        public List<string> providerDetails { get; set; }
        public Hotels hotels;
        public Source source;
    }
}
