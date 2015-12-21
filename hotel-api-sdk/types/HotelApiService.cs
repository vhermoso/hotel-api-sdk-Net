namespace com.hotelbeds.distribution.hotel_api_sdk.types
{
    public static class HotelApiService
    {
        public static string DEVELOPMENT { get; } = "http://localhost:8181";
        public static string LIVE { get; } = "https://api.hotelbeds.com/hotel-api";
        public static string TEST { get; } = "https://api.test.hotelbeds.com/hotel-api";
    }
}
