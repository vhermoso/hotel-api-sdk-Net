using System;

namespace com.hotelbeds.distribution.hotel_api_model.auto.model
{
    public class Stay
    {
        public string checkIn { get; }
        public string checkOut { get; }
        public int shiftDays { get; }
        public bool allowOnlyShift { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        /// <param name="shiftDays"></param>
        /// <param name="allowOnlyShift"></param>
        public Stay(DateTime checkIn, DateTime checkOut, int shiftDays, bool allowOnlyShift)
        {
            this.checkIn = checkIn.ToString("yyyy-MM-dd");
            this.checkOut = checkOut.ToString("yyyy-MM-dd");
            this.shiftDays = shiftDays;
            this.allowOnlyShift = allowOnlyShift;
        }
    }
}
