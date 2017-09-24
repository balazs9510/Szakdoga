using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WorkoutPlaner.Converters
{
    public class DifficultyToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is int))
            {
                throw new ArgumentException("Argument must be int");
            }
            int diff = (int)value;

            switch (diff)
            {
                case 0: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.g_dot.png");
                case 1: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.gg_dot.png");
                case 2: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.ggy_dot.png");
                case 3: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.ggyo_dot.png");
                case 4: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.ggyor_dot.png");
                case 5: return ImageSource.FromResource("WorkoutPlaner.Images.Difficulty.ggyorb_dot.png");
                default: throw new ArgumentOutOfRangeException("Difficulty must be in range [1,5]");
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
