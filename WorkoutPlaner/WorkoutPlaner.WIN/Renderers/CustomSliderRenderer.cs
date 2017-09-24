using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;
//[assembly: ExportRenderer(typeof(Slider), typeof(CustomSliderRenderer))]
namespace WorkoutPlaner.WIN.Renderers
{
    public class CustomSliderRenderer : ViewRenderer<Slider, Windows.UI.Xaml.Controls.Slider>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Slider> e)
        {
            base.OnElementChanged(e);
            if (Control == null && e.NewElement != null)
            {
                var slider = new Windows.UI.Xaml.Controls.Slider();

                SetNativeControl(slider);
            }
            var colorStops = new GradientStopCollection
            {
                new GradientStop() { Color = Windows.UI.Colors.LightGreen, Offset = 0 },
                new GradientStop() { Color = Windows.UI.Colors.Green, Offset = 0.20 },
                new GradientStop() { Color = Windows.UI.Colors.Yellow, Offset = 0.4 },
                new GradientStop() { Color = Windows.UI.Colors.Orange, Offset = 0.6 },
                new GradientStop() { Color = Windows.UI.Colors.OrangeRed, Offset = 0.8 },
                new GradientStop() { Color = Windows.UI.Colors.Red, Offset = 1 }
            };
            var transparent = new SolidColorBrush(Windows.UI.Colors.Transparent);
            var gradinets = new LinearGradientBrush(colorStops, 0);
         
            Control.Value = e.NewElement.Value;
            Control.Maximum = e.NewElement.Maximum;
            Control.Minimum = e.NewElement.Minimum;
            Control.Foreground = gradinets;
            Control.Background = transparent;
            Control.Style = Windows.UI.Xaml.Application.Current.Resources["CustomSliderStyle"] as Windows.UI.Xaml.Style;
        }
    }
}
