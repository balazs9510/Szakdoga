using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Sync;
using Xamarin.Forms;

namespace WorkoutPlaner.Views.Popups
{
    public class SyncResultPopUp : PopupPage
    {
        public SyncResultPopUp(List<SyncMessage> messages)
        {
            var layout = new StackLayout()
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                WidthRequest = 350,
                HeightRequest = 200,
                Padding = 5,
                BackgroundColor = Color.White
            };
            layout.Children.Add(new Label { Text = AppResources.SyncTitle, TextColor = Color.Black, FontSize = 32 });
            foreach (var item in messages)
            {
                var grid = new Grid
                {
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition
                        {
                            Width = GridLength.Auto
                        },
                        new ColumnDefinition
                        {
                            Width = 30
                        }
                    }
                };
                var messageLabel = new Label
                {
                    Text = item.Result
                };
                var image = new Image
                {
                    Aspect = Aspect.AspectFit,
                    WidthRequest = 20,
                    HeightRequest = 20,
                    HorizontalOptions = LayoutOptions.End
                };
                if (item.IsSucessfull)
                    image.Source = ImageSource.FromResource("WorkoutPlaner.Images.succes.png");
                else
                    image.Source = ImageSource.FromResource("WorkoutPlaner.Images.false.png");

                grid.Children.Add(messageLabel, 0, 0);
                grid.Children.Add(image, 1, 0);

                layout.Children.Add(grid);
            }
            Content = layout;
        }
    }
}
