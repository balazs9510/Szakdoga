using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class HistoryPage : BasePage
    {
        public HistoryPage() : base()
        {
        }
        protected override void SetPageToVM()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
        }
    }
}
