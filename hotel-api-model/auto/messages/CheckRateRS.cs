using System;
using System.Collections.Generic;
using com.hotelbeds.distribution.hotel_api_model.auto.model;

namespace com.hotelbeds.distribution.hotel_api_model.auto.messages
{
    public class CheckRateRS : AbstractGenericResponse
    {
        public List<string> providerDetails { get; set; }
        public Hotel hotel { get; set; }
        public Source source { get; set; }
    }
}
