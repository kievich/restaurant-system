using restaurant_system.Models;
using System;

namespace restaurant_system.Tools
{
    public static class NameHelper
    {
        public static string GetNameFromEmail(string value)
        {
            String[] parts = value.Split(new[] { '@' });
            String username = parts[0];
            return username;
        }

        public static string BoolToString(bool value)
        {
            return value ? "Да" : "Нет";
        }

        public static string OrderStatusToString(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Draft:
                    return "Шаблон";
                case OrderStatus.Unassigned:
                    return "Неназначенный";
                case OrderStatus.Assigned:
                    return "Назначенный";
                case OrderStatus.Сanceled:
                    return "Отмененный";
                case OrderStatus.Сompleted:
                    return "Завершенный";
            }

            return "";
        }
    }
}
