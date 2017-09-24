using System;
using System.Globalization;
using Xamarin.Forms;

namespace WorkoutPlaner.Converters
{
    public class StrintToImageSourceConverter :  IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException();
            var str = value as string;
            return ImageSource.FromResource("WorkoutPlaner.Images." + str + ".png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
