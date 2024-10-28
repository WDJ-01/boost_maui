using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boost.Common.Converters
{
    public class BoolToOppositeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Return the opposite value
                return !boolValue;
            }

            return false; // Default in case the value is not a boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
            {
                // Return the opposite of the converted value
                return !boolValue;
            }

            return false;
        }
    }
}
