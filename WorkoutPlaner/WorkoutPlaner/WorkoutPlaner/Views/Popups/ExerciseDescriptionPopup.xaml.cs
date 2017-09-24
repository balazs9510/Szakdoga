using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WorkoutPlaner.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExerciseDescriptionPopup : PopupPage
    {
        public ExerciseDescriptionPopup(object bindingContext)
        {
            this.BindingContext = bindingContext;
            InitializeComponent();
            Content.BackgroundColor = Color.Transparent;
        }
    }
}