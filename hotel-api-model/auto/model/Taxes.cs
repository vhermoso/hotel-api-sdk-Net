using System.Collections.Generic;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Taxes
    {
        public List<Tax> taxes { get; set; }
        public bool allIncluded { get; set; }
    }
}
