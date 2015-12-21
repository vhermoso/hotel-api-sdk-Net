using System;
using com.hotelbeds.distribution.hotel_api_model.auto.common;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Booking
    {
        public string reference { get; set; }
        public string cancellationReference { get; set; }
        public string clientReference { get; set; } 
	    public DateTime creationDate { get; set; }
        //[JsonProperty("paymentType", Required = Required.Always)]
        //[JsonConverter(typeof(SimpleTypes.PaymentTypeConverter))]
        public SimpleTypes.BookingStatus status { get; set; }    
	    public decimal agCommision { get; set; }    
	    public decimal commisionVAT { get; set; }
        public string creationUser { get; set; }
        public Holder holder { get; set; }
        public Hotel hotel { get; set; }
        public string remark { get; set; }
    }
}
