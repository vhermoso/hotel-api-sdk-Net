using System;
using System.Collections.Generic;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Hotel
    {        
        public DateTime checkIn { get; set; }
        public DateTime checkOut { get; set; }
        public int code { get; set; }
        public string name { get; set; }
        public string categoryCode { get; set; }
        public string categoryName { get; set; }
        public string destinationCode { get; set; }
        public string destinationName { get; set; }
        public short zoneCode { get; set; }
        public string zoneName { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string giata { get; set; }
        public List<Keyword> keywords { get; set; }
        public List<Review> reviews { get; set; }
        public List<Room> rooms { get; set; }
        public decimal minRate { get; set; }
        public decimal maxRate { get; set; }
        public decimal totalSellingRate { get; set; }
        public decimal totalNet { get; set; }
        public decimal pendingAmount { get; set; }
        public string currency { get; set; }
        public List<CreditCard> creditCards { get; set; }
        public Supplier supplier { get; set; }
        public string clientComments { get; set; }
        public Upselling upselling { get; set; }
    }
}
