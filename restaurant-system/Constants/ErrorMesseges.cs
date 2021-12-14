namespace restaurant_system
{
    public static class ErrorMesseges
    {
        public const string ErrorTitle = "Error";
        public const string Common = "Something went wrong";
        public const string DishPrice = "Dish price can't be less than 0";
        public const string ReservationLessThanNull = "The End date less then Start or equals";
        public const string ReservationMaxHour = "Max Reservation Hours - {0}";
        public const string ReservationExists = "The reservation on this date and time already exists";
        public const string EventCustomerExists = "This customer already exists in the event";

    }
}
