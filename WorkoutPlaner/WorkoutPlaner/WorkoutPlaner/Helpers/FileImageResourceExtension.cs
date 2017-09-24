using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutPlaner.Helpers
{
    [ContentProperty("Source")]
    public class FileImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }
            // Do your translation lookup here, using whatever method you require
            var imageSource = new FileImageSource() { File = "WorkoutPlaner.Images." + Source };

            return imageSource;
        }
    }
}
