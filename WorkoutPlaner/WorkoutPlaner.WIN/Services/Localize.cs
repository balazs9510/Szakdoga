using System.Globalization;
using Windows.Globalization;
using WorkoutPlaner.Services;
using WorkoutPlaner.WIN.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(Localize))]
namespace WorkoutPlaner.WIN.Services
{
    public class Localize : ILocalize
    {
        public CultureInfo GetCurrentCultureInfo()
        {
            return CultureInfo.CurrentUICulture;
        }

        public void SetLocale(CultureInfo ci)
        {
            ApplicationLanguages.PrimaryLanguageOverride = ci.NativeName;
        }
    }
}
