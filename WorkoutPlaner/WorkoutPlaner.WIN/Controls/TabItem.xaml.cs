using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace WorkoutPlaner.WIN.Controls
{
    public partial class TabItem : UserControl
    {
        public static readonly DependencyProperty IconProperty =
               DependencyProperty.Register("Icon", typeof(ImageSource), typeof(TabItem), null);

        public ImageSource Icon
        {
            get { return GetValue(IconProperty) as ImageSource; }
            set { SetValue(IconProperty, value); }
        }

        public string Label
        {
            get { return GetValue(LabelProperty) as string; }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(TabItem), null);

        public TabItem()
        {
            this.InitializeComponent();
        }
    }
}
