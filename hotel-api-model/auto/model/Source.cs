using System;
using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.util;
using Newtonsoft.Json;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Source
    {
        [JsonProperty("channel", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.ChannelTypeConverter))]
        public SimpleTypes.ChannelType? channel { get; set; }
        [JsonProperty("device", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.DeviceTypeConverter))]
        public SimpleTypes.DeviceType? device { get; set; }
        public String deviceInfo { get; set; }
        
    }
}
