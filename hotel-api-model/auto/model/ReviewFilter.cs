using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.util;
using Newtonsoft.Json;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class ReviewFilter
    {
        [JsonProperty("type", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.AccommodationTypeConverter))]
        public SimpleTypes.ReviewsType? type { get; set; }
        public decimal minRate { get; set; }
        public decimal maxRate { get; set; }
        public int minReviewCount { get; set; }
    }
}
