using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using WorkoutPlaner.Controls;
using WorkoutPlaner.EventArgs;
using WorkoutPlaner.WIN.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CheckBox), typeof(CheckBoxRenderer))]
namespace WorkoutPlaner.WIN.Renderers
{
    public class CheckBoxRenderer : ViewRenderer<CheckBox, Windows.UI.Xaml.Controls.CheckBox>
    {
        /// <summary>
        /// Called when [element changed].
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnElementChanged(ElementChangedEventArgs<CheckBox> e)
        {
            base.OnElementChanged(e);

            if (Control == null && e.NewElement != null)
            {
                var checkBox = new Windows.UI.Xaml.Controls.CheckBox();
                checkBox.Checked += (s, args) => Element.Checked = true;
                checkBox.Unchecked += (s, args) => Element.Checked = false;

                SetNativeControl(checkBox);
            }

            if (e.NewElement == null || this.Control == null) return;
            if (!string.IsNullOrEmpty(e.NewElement.Text))
                Control.Content = e.NewElement.Text;
            Control.IsChecked = e.NewElement.Checked;
            Control.IsEnabled = e.NewElement.IsEnabled;
            Control.Visibility = e.NewElement.IsVisible ? Visibility.Visible : Visibility.Collapsed;
            Control.Margin = new Windows.UI.Xaml.Thickness(0, 10, 10, 10);
            //Control.Foreground = ConvertColor(e.NewElement.TextColor);

            UpdateFont();
        }

        /// <summary>
        /// Handles the <see cref="E:ElementPropertyChanged" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Checked":
                    Control.IsChecked = Element.Checked;
                    break;
                case "TextColor":
                    Control.Foreground = ConvertColor(Element.TextColor);
                    break;
                case "FontName":
                case "FontSize":
                    UpdateFont();
                    break;
                case "CheckedText":
                case "UncheckedText":
                    Control.Content = Element.Text;
                    break;
                default:
                    base.OnElementPropertyChanged(sender, e);
                    break;
            }
        }
        private bool Checked(object sender, RoutedEventArgs eventArgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Control.Content = Element.Text;
                Control.IsChecked = true;
            });
            return true;
        }
        /// <summary>
        /// Checkeds the changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void CheckedChanged(object sender, CheckBoxEventArgs eventArgs)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Control.Content = Element.Text;
                Control.IsChecked = eventArgs.Checked;
            });
        }
        private SolidColorBrush ConvertColor(Xamarin.Forms.Color color)
        {
            var colorConverter = new ColorConverter();
            Windows.UI.Color wpColor = Windows.UI.Color.FromArgb(
                     (byte)(color.A * 255),
                     (byte)(color.R * 255),
                     (byte)(color.G * 255),
                     (byte)(color.B * 255));
            return new SolidColorBrush(wpColor); ;
        }
        /// <summary>
        /// Updates the font.
        /// </summary>
        private void UpdateFont()
        {
            if (!string.IsNullOrEmpty(Element.FontName))
            {
                Control.FontFamily = new FontFamily(Element.FontName);
            }

            Control.FontSize = (Element.FontSize > 0) ? (float)Element.FontSize : 12.0f;
        }
    }
}
