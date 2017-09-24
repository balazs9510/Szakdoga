using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.Views.Popups
{
    public partial class ExerciseSerialRepsPopup : PopupPage
    {
        private ExerciseItem _currentItem;
        public ExerciseSerialRepsPopup(ExerciseItem currentItem)
        {
            InitializeComponent();
            Content.BackgroundColor = Color.Transparent;
            RepsLabel.Text = currentItem.Reps.ToString();
            SerialLabel.Text = currentItem.Serial.ToString();
            RepsStepper.Value = currentItem.Reps;
            SerialStepper.Value = currentItem.Serial;
            _currentItem = currentItem;
        }
        public ExerciseSerialRepsPopup()
        {
            InitializeComponent();
            Content.BackgroundColor = Color.Transparent;
        }
        public ICommand CloseCommand => new Command(() => { CloseAsync(); });

        private async void CloseAsync()
        {
            await Navigation.PopPopupAsync(true);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected override bool OnBackButtonPressed()
        {
            // Prevent hide popup
            return base.OnBackButtonPressed();
        }

        // Invoced when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return default value - CloseWhenBackgroundIsClicked
            return base.OnBackgroundClicked();
        }

        private void SerialStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var serial = ((Stepper)sender).Value;
            SerialLabel.Text = serial.ToString();
        }

        private void RepsStepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var reps = ((Stepper)sender).Value;
            RepsLabel.Text = reps.ToString();
        }

        private async void SaveAsync(object sender, System.EventArgs e)
        {
            _currentItem.Reps = (int)RepsStepper.Value;
            _currentItem.Serial = (int)SerialStepper.Value;
            if (_currentItem.Id == null)
                MessagingCenter.Send(this, MessagingKeys.ExerciseRepsSerialAdded, _currentItem);
            else
                MessagingCenter.Send(this, MessagingKeys.ExerciseItemEdited, _currentItem);
            await Navigation.PopPopupAsync(true);
        }
    }
}