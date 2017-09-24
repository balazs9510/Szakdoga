using Rg.Plugins.Popup.Extensions;
using System;
using System.Windows.Input;
using WorkoutPlaner.Resources;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class LoginPage : BasePage
    {
        public ICommand LoginOfflineCommand => new Command(() => OpenSelectUserPopUpAsync());

        public LoginPage() : base()
        {
            
        }

        private async void OpenSelectUserPopUpAsync()
        {
            var VM = (LoginPageViewModel)BindingContext;

            if(VM.Users.Count > 0)
            {
                var popUp = new SelectUserPopUp(this.BindingContext);
                await this.Navigation.PushPopupAsync(popUp, true);
            }
            else
            {
                await DisplayAlert(AppResources.NoUserTitle, AppResources.NoUserMessage, AppResources.Ok);
            }
            
        }

        protected override void SetPageToVM()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
        }
        
    }
}
