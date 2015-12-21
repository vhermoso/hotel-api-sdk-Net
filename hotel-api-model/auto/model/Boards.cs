using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using com.hotelbeds.distribution.hotel_api_model.util;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Boards
    {
        public List<String> board { get; set; }
        [JsonProperty("included", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.BooleanConverter))]
        public bool? included { get; set; }
    }    
}
