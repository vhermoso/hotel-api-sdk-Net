using System.Collections.Generic;
using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.util;
using Newtonsoft.Json;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class HotelsFilter
    {
        public List<int> hotel { get; set; }
        [JsonProperty("included", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.BooleanConverter))]
        public bool included { get; set; }
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.HotelCodeTypeConverter))]
        public SimpleTypes.HotelCodeType? type { get; set; }        
    }
}
