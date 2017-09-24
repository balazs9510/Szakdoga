using System;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.ViewModels;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            var VM = (MainPageViewModel)BindingContext;
            VM.Page = this.CurrentPage;
            if (Application.Current.Properties.ContainsKey(StoreKeys.AppMode))
            {
                var mode = (ApplicationMode)Application.Current.Properties[StoreKeys.AppMode];
                switch (mode)
                {
                    case ApplicationMode.Online:
                        var profilPage = new ProfilPage();
                        var toolbarItem = new ToolbarItem()
                        {
                            Order = ToolbarItemOrder.Primary,
                            Priority = 0,
                            Command = VM.LogoutCommand
                        };
                        switch (Device.RuntimePlatform)
                        {
                            case Device.Android:
                                profilPage.Icon = "menu_profile";
                                toolbarItem.Icon = "logout";
                                break;
                            case Device.Windows:
                                profilPage.Icon = "Assets/menu_profile.png";
                                toolbarItem.Icon = "Images/logout.png";
                                break;
                            default:
                                profilPage.Icon = "Assets/menu_profile.png";
                                toolbarItem.Icon = "Images/logout.png";
                                break;
                        }
                        Children.Add(profilPage);

                        ToolbarItems.Add(toolbarItem);
                        break;
                }
            }
        }
    }
}
