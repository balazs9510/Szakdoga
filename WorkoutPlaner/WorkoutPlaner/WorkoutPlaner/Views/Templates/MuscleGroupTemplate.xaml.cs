using Xamarin.Forms;

namespace WorkoutPlaner.Views.Templates
{
    public partial class MuscleGroupTemplate : ContentView
	{
		public MuscleGroupTemplate (object bindingContext)
		{
            BindingContext = bindingContext;
            InitializeComponent();
        }
	}
}