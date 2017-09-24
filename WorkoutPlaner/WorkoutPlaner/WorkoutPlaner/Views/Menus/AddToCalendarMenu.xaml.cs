using SlideOverKit;
using System;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels;
using Xamarin.Forms;

namespace WorkoutPlaner.Views.Menus
{
    public partial class AddToCalendarMenu : SlideMenuView
    {
        public AddToCalendarMenu()
        {
            InitializeComponent();
            MenuOrientations = MenuOrientation.BottomToTop;
            IsFullScreen = true;
            if (Device.RuntimePlatform.Equals(Device.Android))
                HeightRequest += 50;
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            var VM = (WorkoutDetailPageViewModel)this.BindingContext;
            var date = new DateTime(datePicker.Date.Ticks);
            date = date.AddTicks(timePicker.Time.Ticks);
            DependencyService.Get<ICalendarService>().SaveWorkoutAsync(VM.Workout.Name,date);
            HideWithoutAnimations();
        }
    }
}