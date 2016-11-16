using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_model.auto.messages;
using com.hotelbeds.distribution.hotel_api_sdk;
using com.hotelbeds.distribution.hotel_api_sdk.helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hotel_api_sdk_demo
{
    class Tests
    {
        public static void ejecutar()
        {
            cancelacion();
            Console.ReadLine();
        }

        public static void reserva()
        {
            HotelApiClient client = new HotelApiClient();

            string rateKey = "20161030|20161031|W|228|9353|DBL.ST|NRF-1530COM18|RO||1~2~0||N@77814B4F3B42437685714CCE99CCE1F2";
            
            ConfirmRoom confirmRoom = new ConfirmRoom();
            confirmRoom.details = new List<RoomDetail>();
            confirmRoom.detailed(RoomDetail.GuestType.ADULT, 30, "NombrePasajero1", "ApellidoPasajero1", 1);
            confirmRoom.detailed(RoomDetail.GuestType.ADULT, 30, "NombrePasajero2", "ApellidoPasajero2", 1);

            // CHECKRATES
            BookingCheck bookingCheck = new BookingCheck();
            bookingCheck.addRoom(rateKey, confirmRoom);
            CheckRateRQ checkRateRQ = bookingCheck.toCheckRateRQ();
            checkRateRQ.upselling = false;

            CheckRateRS responseRate = client.doCheck(checkRateRQ);
            Console.WriteLine("CHECKRATES RS:");
            string stringResponse = JsonConvert.SerializeObject(responseRate, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            responseRate  = JsonConvert.DeserializeObject<CheckRateRS>(stringResponse);
            Console.WriteLine(JsonConvert.SerializeObject(responseRate, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore }));


            Console.ReadLine();

            //BOOKING 
            Booking booking = new Booking();
            booking.createHolder("Rosetta", "Pruebas");
            booking.clientReference = "SDK Test";
            booking.remark = "***SDK***TESTING";

            ////NOTE: ONLY LIBERATE (PAY AT HOTEL MODEL) USES PAYMENT DATA NODES. FOR OTHER PRICING MODELS THESE NODES MUST NOT BE USED.
            //booking.cardType = "VI";
            //booking.cardNumber = "4444333322221111";
            //booking.expiryDate = "0620";
            //booking.cardCVC = "0620";
            //booking.email = "pmayol@multinucleo.com";
            //booking.phoneNumber = "654654654";
            //booking.cardHolderName = "AUTHORISED";

            booking.addRoom(rateKey, confirmRoom);
            BookingRQ bookingRQ = booking.toBookingRQ();
            bookingRQ.language = "CAS";

            if (bookingRQ != null)
            {
                BookingRS responseBooking = client.confirm(bookingRQ);
                Console.WriteLine("Booking Response:");
                if (responseBooking != null)
                    Console.WriteLine(JsonConvert.SerializeObject(responseBooking, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore }));
                else
                    Console.WriteLine("ResponseBooking Object Response is null");
            }
        }

        public static void bookingDetail()
        {
            string bookingReference = "102-7115213";
            string language = "CAS";
            List<Tuple<string, string>> param = new List<Tuple<string, string>>
            {
                new Tuple<string, string>("${bookingId}", bookingReference),
                new Tuple<string, string>("${language}", language)
            };

            BookingDetailRS bookingDetailRS = new HotelApiClient().Detail(param);

            Console.WriteLine("RESPUESTA DETAILS:");
            Console.WriteLine(JsonConvert.SerializeObject(bookingDetailRS, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }

        public static void cancelacion()
        {
            string bookingReference = "102-7115215";
            List<Tuple<string, string>> param = new List<Tuple<string, string>>
                                    {
                                        new Tuple<string, string>("${bookingId}", bookingReference),
                                        new Tuple<string, string>("${flag}", "SIMULATION")
                                    };

            BookingCancellationRS bookingCancellationRS = new HotelApiClient().Cancel(param);

            Console.WriteLine("RESPUESTA CANCELACION:");
            Console.WriteLine(JsonConvert.SerializeObject(bookingCancellationRS, Formatting.Indented, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Include }));          
        }
    }
}
