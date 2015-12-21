using System;
using System.Collections.Generic;

using com.hotelbeds.distribution.hotel_api_model.auto.messages;
using com.hotelbeds.distribution.hotel_api_model.auto.model;
using com.hotelbeds.distribution.hotel_api_model.auto.common;
using com.hotelbeds.distribution.hotel_api_sdk.types;

namespace com.hotelbeds.distribution.hotel_api_sdk.helpers
{
    public class BookingCheck
    {
        public List<ConfirmRoom> rooms;
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public CheckRateRQ toCheckRateRQ()
        {
            try
            {
                CheckRateRQ checkRateRQ = new CheckRateRQ();
                checkRateRQ.rooms = new List<hotel_api_model.auto.model.BookingRoom>();

                for (int i = 0; i < this.rooms.Count; i++)
                {
                    BookingRoom bookingRoom = new BookingRoom();
                    bookingRoom.rateKey = rooms[i].rateKey;
                    bookingRoom.paxes = new List<Pax>();
                    Pax[] paxes = new Pax[this.rooms[i].details.Count];
                    for (int d = 0; d < this.rooms[i].details.Count; d++)
                    {
                        Pax pax = new Pax();
                        pax.type = rooms[i].details[d].getType() == RoomDetail.GuestType.ADULT ? SimpleTypes.HotelbedsCustomerType.AD : SimpleTypes.HotelbedsCustomerType.CH;
                        pax.age = rooms[i].details[d].getAge();
                        pax.name = rooms[i].details[d].getName();
                        pax.surname = rooms[i].details[d].getSurname();
                        paxes[d] = pax;
                    }
                    bookingRoom.paxes.AddRange(paxes);
                    checkRateRQ.rooms.Add(bookingRoom);
                }

                checkRateRQ.Validate();

                return checkRateRQ;
            }
            catch(HotelSDKException e)
            {
                throw e;
            }
        }

        public void addRoom(string rateKey, ConfirmRoom confirmRoom)
        {
            if (this.rooms == null)
                this.rooms = new List<ConfirmRoom>();

            confirmRoom.rateKey = rateKey;
            this.rooms.Add(confirmRoom);
        }
    }
}
