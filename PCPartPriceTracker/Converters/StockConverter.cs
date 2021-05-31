using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace PCPartPriceTracker.Converters
{
    public class StockConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool InStock = (bool)value;
            return InStock ? "In Stock" : "Out of Stock";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stock = (string)value;
            if (stock.Equals("In Stock"))
            {
                return true;
            }
            else;
            {
                return false;
            }
        }
    }
}
