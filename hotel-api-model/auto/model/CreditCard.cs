using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.util;
using Newtonsoft.Json;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class CreditCard
    {
        public string code { get; set; }
        public string name { get; set; }
        [JsonProperty("paymentType", Required = Required.Always)]
        [JsonConverter(typeof(JSonConverters.PaymentTypeConverter))]
        public SimpleTypes.PaymentType paymentType { get; set; }
    }
}
