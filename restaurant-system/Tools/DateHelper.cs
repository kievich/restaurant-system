using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Tools
{
    public static class DateHelper
    {
        public static DateTime Parse(string value)
        {
            try
            {
                DateTime convertedValue = DateTime.Parse(value);
                return convertedValue;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }
    }
}
