using System.Diagnostics;
using System.Linq;
using WorkoutPlaner.WIN.Controls;
using WorkoutPlaner.WIN.Extensions;
using WorkoutPlaner.WIN.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(CustomTabbedPageRenderer))]
namespace WorkoutPlaner.WIN.Renderers
{
    public class CustomTabbedPageRenderer : TabbedPageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                Debug.WriteLine("No FormsPivot found. Badge not added.");
                return;
            }
            for (var i = 0; i < Control.Items.Count; i++)
            {
                AddTabBadge(i);
            }
        }

        private void AddTabBadge(int tabIndex)
        {
            if (Element == null)
            {
                return;
            }

            var element = Element.Children[tabIndex];

            if (element != null)
            {
                var dataTemplate = Windows.UI.Xaml.Application.Current.Resources["TabbedPageHeaderTemplate"] as
                   Windows.UI.Xaml.DataTemplate;

                Control.HeaderTemplate = dataTemplate;
                Control.SelectionChanged += OnControlSelectionChanged;

                element.PropertyChanged += OnTabbedPagePropertyChanged;
            }
        }

        private void OnControlSelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
        {
            SetSelectedTab((FormsPivot)sender);
        }

        protected virtual void OnTabbedPagePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var element = sender as Element;

            if (element == null)
                return;

            var tabItems = Control.GetDescendantsOfType<TabItem>();
            var tabItem = tabItems.FirstOrDefault(t => t.Label == ((Page)element).Title);

            if (tabItem == null)
            {
                return;
            }
            SetSelectedTab(Control);
        }

        private void SetSelectedTab(FormsPivot pivot)
        {
            var tabItems = Control.GetDescendantsOfType<TabItem>();
            var tabItem = tabItems.FirstOrDefault(t => t.Label == ((Page)pivot.SelectedItem).Title);

            if (tabItem == null)
            {
                return;
            }

            foreach (var item in tabItems)
                item.Opacity = 0.5;

            tabItem.Opacity = 1.0;
        }
    }
}
