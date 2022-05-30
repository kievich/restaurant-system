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
    }
}
