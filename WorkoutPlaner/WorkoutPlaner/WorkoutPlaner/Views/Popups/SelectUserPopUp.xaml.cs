using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutPlaner.Views.Popups
{
    public partial class SelectUserPopUp : PopupPage
    {
        public ICommand MakeBorderCommand => new Command((o) =>
        {
            var Vm = (LoginPageViewModel)BindingContext;
            if (((ApplicationUser)o).Id.Equals(Vm.SelectedUser.Id))
            {
                var gridElements = listOfUsers.Children.GetEnumerator();
                while (gridElements.MoveNext())
                {
                    if (gridElements.Current.BindingContext is ApplicationUser user)
                    {
                        if (user.Id.Equals(Vm.SelectedUser.Id))
                            gridElements.Current.BackgroundColor = Color.Green;
                        else
                            gridElements.Current.BackgroundColor = Color.LightGreen;
                    }
                }
            }

        });
        Grid listOfUsers;
        public SelectUserPopUp()
        {
            InitializeComponent();
            Content.BackgroundColor = Color.Transparent;
        }
        public SelectUserPopUp(object bindingContext)
        {
            InitializeComponent();
            this.BindingContext = bindingContext;
            var VM = (LoginPageViewModel)BindingContext;
            listOfUsers = new Grid()
            {
                BackgroundColor = Color.White,
                HeightRequest = 300,
                WidthRequest = 100,
                Padding = 5,
                RowSpacing = 2,
            };
            listOfUsers.Children.AddVertical(new Label()
            {
                Text = AppResources.Choose,
                FontSize = 26,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.CenterAndExpand,

            });
            for (int i = 0; i < VM.Users.Count; i++)
            {
                var user = VM.Users[i];
                var nameLabel = new Label()
                {
                    BindingContext = user,
                    FontSize = 20,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.Black,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Color.LightGreen
                };


                nameLabel.SetBinding(Label.TextProperty, path: nameof(ApplicationUser.Name));
                nameLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    BindingContext = VM,
                    Command = VM.SelectUserCommand,
                    CommandParameter = user
                });
                nameLabel.GestureRecognizers.Add(new TapGestureRecognizer()
                {
                    NumberOfTapsRequired = 1,
                    Command = MakeBorderCommand,
                    CommandParameter = user,
                });
                listOfUsers.Children.AddVertical(nameLabel);
            }
            StackLayout content = new StackLayout()
            {
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            content.Children.Add(listOfUsers);
            listOfUsers.Children.AddVertical(new Button()
            {
                BackgroundColor = Color.LightGreen,
                VerticalOptions = LayoutOptions.EndAndExpand,
                Text = AppResources.Next,
                FontSize = 20,
                Command = VM.LoginOfflineCommand
            });
            Content = content;
            Content.BackgroundColor = Color.Transparent;
            SubscribeEvents();

        }
        public void NextClick(object sender, System.EventArgs e)
        {
            var VM = (LoginPageViewModel)BindingContext;
            VM.LoginOfflineCommand.Execute(null);
        }
        private void SubscribeEvents()
        {
            MessagingCenter.Subscribe<LoginPageViewModel>(this, MessagingKeys.ClosePopUp, async (s) =>
            {
                await Navigation.PopPopupAsync(true);
            });
        }
    }
}