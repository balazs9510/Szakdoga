using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using WorkoutPlaner.Services;
using Xamarin.Forms;

namespace WorkoutPlaner.Converters
{
    public class TranslateConverter : IValueConverter
    {
        readonly CultureInfo ci;
        const string ResourceId = "WorkoutPlaner.Resources.AppResources";

        private Lazy<ResourceManager> ResMgr = new Lazy<ResourceManager>(() => new ResourceManager(ResourceId
                                                                                                                  , typeof(TranslateConverter).GetTypeInfo().Assembly));
        public TranslateConverter()
        {
            ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
                throw new ArgumentException("Must be a string");
            var str = value as string;
            if (string.IsNullOrEmpty(str))
                return "";

            var translation = ResMgr.Value.GetString(str, ci);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(
                    String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", str, ResourceId, ci.Name),
                    "Text");
#else
                translation = Text; // returns the key, which GETS DISPLAYED TO THE USER
#endif
            }
            return translation;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
