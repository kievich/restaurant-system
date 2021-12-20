using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant_system.Tools
{
    public static class DecimalHelper
    {
        public static decimal Parse(string value)
        {
            try
            {
                decimal convertedValue = Decimal.Parse(value, CultureInfo.InvariantCulture);
                return convertedValue;
            }
            catch (Exception)
            {
                return Decimal.Zero;
            }
        }
    }
}
