using SlideOverKit;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views.Menus;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class WorkoutDetailPage : MenuContainerPage
    {
        public ICommand OpenSaveCalendarCommand => new Command(() => ShowMenu());
        public WorkoutDetailPage() : base()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
            SlideMenu = new AddToCalendarMenu();
            MessagingCenter.Subscribe<WorkoutDetailPageViewModel>(this, MessagingKeys.DeleteWorkoutPopUp, async (s) =>
            {
                bool answer = await DisplayAlert(AppResources.SureDelete, "",
                    AppResources.Ok, AppResources.Cancel);
                if (answer)
                   ((WorkoutDetailPageViewModel)BindingContext).DeleteWorkoutAsync();
            });
            NavigationPage.SetHasBackButton(this, true);
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<WorkoutDetailPageViewModel>(this, MessagingKeys.DeleteWorkoutPopUp);
        }
    }
}
