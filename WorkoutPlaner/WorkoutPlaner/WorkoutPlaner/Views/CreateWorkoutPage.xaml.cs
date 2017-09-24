using Rg.Plugins.Popup.Extensions;
using SlideOverKit;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.Views.Menus;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class CreateWorkoutPage : MenuContainerPage
    {
        public ICommand OpenMenuCommand => new Command(() => ShowMenu());
        public CreateWorkoutPage() : base()
        {
            InitializeComponent();
            NavigationPage.SetHasBackButton(this, true);
            MessagingCenter.Subscribe<CreateWorkoutPageViewModel, Exercise>(this, MessagingKeys.EditExerciseItem, (sender, args) =>
            {
                Navigation.PushPopupAsync(new ExerciseSerialRepsPopup(new ExerciseItem(args)));
            });
            CustomNavigationPage.SetHasBackButton(this, true);
            this.SlideMenu = new AddedExerciseMenu(BindingContext);
        }

        protected override bool OnBackButtonPressed()
        {
            var VM = (CreateWorkoutPageViewModel)BindingContext;
            VM.OnNavigatedFrom(new Prism.Navigation.NavigationParameters());
            return base.OnBackButtonPressed();
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CreateWorkoutPageViewModel, Exercise>(this, MessagingKeys.EditExerciseItem);
        }
    }
}
