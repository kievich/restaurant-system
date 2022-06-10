namespace restaurant_system
{
    public static class ErrorMesseges
    {
        public const string ErrorTitle = "Ошибка";
        public const string Common = "Что-то пошло не так";
        public const string DishPrice = "Цена не может быть меньше нуля";
        public const string ReservationLessThanNull = "Дата завершения меньше или равна дате начала";
        public const string ReservationMaxHour = "Максимальная продожительность бронирования - {0}ч";
        public const string ReservationExists = "Бронирование на эту дату уже существует";
        public const string EventCustomerExists = "Клиент уже добавлен в событие";

    }
}
