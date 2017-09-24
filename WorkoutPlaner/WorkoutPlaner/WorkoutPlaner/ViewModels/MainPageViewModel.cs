using Microsoft.EntityFrameworkCore;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.Sync;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        public Command LogoutCommand => new Command(() => LogoutAsync());

        private async void LogoutAsync()
        {
            IsBusy = true;
            var networkService = new NetworkService();
            var response = await networkService.PostLogout();
            if (response.IsSuccessStatusCode)
                DisplaySimpleMessageAsync(AppResources.SuccessLogout);
            NavigateAsync(nameof(LoginPage));
            IsBusy = false;
        }

        public MainPageViewModel(INavigationService navigationService, IDatabaseService dbService) :
            base(navigationService, dbService)
        {
        }
      
    }
}
