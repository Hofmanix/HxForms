using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HxForms.BindingConverters
{
    public class InvertBooleanConverter: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean b)
            {
                return !b;
            }
            throw new FormatException("Value is not boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Boolean b)
            {
                return !b;
            }
            throw new FormatException("Value is not boolean");
        }
    }
}
