using WorkoutPlaner.Common;
using WorkoutPlaner.Resources;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class CompleteWorkoutPage : BasePage
    {
        ListViewAlternatingRowProcessor _listViewProcessor;
        public CompleteWorkoutPage() : base()
        {
            Color odd = Color.LightGreen;
            Color even= Color.LimeGreen;
            Color tapped = Color.Green;
            _listViewProcessor =
                new ListViewAlternatingRowProcessor(even, odd, tapped);
                 
        }
        protected override void SetPageToVM()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<CompleteWorkoutPageViewModel>(this, MessagingKeys.StopWorkoutPressed, async (s) =>
            {
                bool answer = await DisplayAlert(AppResources.Warning,
                    AppResources.StopMessage, AppResources.Ok, AppResources.Cancel);
                if (answer)
                {
                    var VM = (CompleteWorkoutPageViewModel)BindingContext;
                    VM.NavigateAsync(nameof(MainPage));
                }
            });
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<CompleteWorkoutPageViewModel>(this, MessagingKeys.StopWorkoutPressed);
        }
        private void ViewCell_Appearing(object sender, System.EventArgs e)
        {
            _listViewProcessor.SetBackColor(sender);

        }
    }
    public class ListViewAlternatingRowProcessor
    {
        private bool _isEvenRow;
        private Color _evenRowColor;
        private Color _oddRowColor;
        private Color _tappedColor;

        private ViewCell _previouslyTappedCell = null;
        private Color? _previouslyTappedCellNaturalBackColor;

        public ListViewAlternatingRowProcessor(Color evenBackColor, Color oddBackColor, Color tappedColor)
        {
            _evenRowColor = evenBackColor;
            _oddRowColor = oddBackColor;
            _tappedColor = tappedColor;
        }

        public void SetBackColor(object viewCellSender)
        {
            var viewCell = (ViewCell)viewCellSender;
            var boxView = viewCell.FindByName<BoxView>("HiderBox");
            Color bg = _oddRowColor;
            viewCell.Tapped += ViewCell_Tapped;

            if (_isEvenRow)
            {
                bg = _evenRowColor;
            }

            _isEvenRow = !_isEvenRow;

            if (viewCell.View != null)
            {
                viewCell.View.BackgroundColor = bg;
                boxView.BackgroundColor = bg;
            }
        }

        private void ViewCell_Tapped(object sender, System.EventArgs e)
        {
            var viewCell = (ViewCell)sender;

            if (_previouslyTappedCellNaturalBackColor.HasValue)
            {
                _previouslyTappedCell.View.BackgroundColor = _previouslyTappedCellNaturalBackColor.Value;
            }

            if (viewCell.View != null)
            {
                _previouslyTappedCellNaturalBackColor = viewCell.View.BackgroundColor;
                viewCell.View.BackgroundColor = _tappedColor;

                _previouslyTappedCell = viewCell;
            }
        }
    }
}
