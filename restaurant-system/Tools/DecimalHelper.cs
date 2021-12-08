using System;
using System.Collections.Generic;
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
                Decimal convertedValue = Convert.ToDecimal(value.Replace(".", ","));
                return convertedValue;
            }
            catch (Exception)
            {
                return Decimal.Zero;
            }
        }
    }
}
