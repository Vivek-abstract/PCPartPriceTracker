using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace PCPartPriceTracker.Converters
{
    public class ReversedBooleanToVisibilityConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            bool val = !(bool)value;

            return val ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
