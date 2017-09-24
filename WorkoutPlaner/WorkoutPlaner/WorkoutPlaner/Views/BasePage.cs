using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WorkoutPlaner.Common;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Sync;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public abstract class BasePage : ContentPage
    {
        public BasePage()
        {
           
            SetPageToVM();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            
        }
        protected abstract void SetPageToVM();
        public void CreateMessage(string title, string message, bool twoButtonMode)
        {
            if (!twoButtonMode)
                DisplayAlert(title, message, AppResources.Cancel);
            else
                DisplayAlert(title, message, AppResources.Ok, AppResources.Cancel);
        }
    }
}