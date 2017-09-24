using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutPlaner.Converters
{
    public class StringToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                throw new ArgumentException("Argument must be a string");
            }
            var str = value as string;
            if (str.Length > 2)
                return str.Substring(0, 1).ToUpper() + str.Substring(1);
            else
                return string.Empty;
            //return str.First().ToString().ToUpper() + str.Substring(1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
