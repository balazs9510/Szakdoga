using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class WorkoutsPage : BasePage
    {
        public WorkoutsPage() : base()
        {
            
            AddBtn.Source = ImageSource.FromResource("WorkoutPlaner.Images.add_btn_rounded.png");
        }
        protected override void SetPageToVM()
        {
            InitializeComponent();
            ((BaseViewModel)BindingContext).Page = this;
        }
    }
}
