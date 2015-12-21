using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.util;
using Newtonsoft.Json;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Pax
    {
        public int? roomId { get; set; }
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.HotelbedsCustomerTypeConverter))]
        public SimpleTypes.HotelbedsCustomerType? type { get; set; }
        public int? age { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
    }
}
