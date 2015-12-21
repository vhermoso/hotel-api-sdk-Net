using Newtonsoft.Json;
using System;
using com.hotelbeds.distribution.hotel_api_model.util;
using System.Collections.Generic;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Rooms
    {
        public List<String> room { get; set; }
        [JsonProperty("included", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.BooleanConverter))]
        public bool? included { get; set; }
    }
}
