using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Occupancy
    {
        public int? rooms { get; set; }
        [JsonProperty("adults", Required = Required.Always)]
        public int? adults { get; set; }
        [DefaultValue(30)]
        public int? children { get; set; }
        public List<Pax> paxes { get; set; }
    }
}
