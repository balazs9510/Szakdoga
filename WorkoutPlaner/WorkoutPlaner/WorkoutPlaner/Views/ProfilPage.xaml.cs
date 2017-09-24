using System;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.ViewModels;
using WorkoutPlaner.ViewModels.Base;
using Xamarin.Forms;

namespace WorkoutPlaner.Views
{
    public partial class ProfilPage : BasePage
    {
        public ProfilPage() : base()
        {
            var VM = (ProfilPageViewModel)BindingContext;
            if (!string.IsNullOrEmpty(VM.User.PhotoPath))
            {
                UserImage.Source = ImageSource.FromFile(VM.User.PhotoPath);
            }
            else
                UserImage.Source = ImageSource.FromResource("WorkoutPlaner.Images.n.png");

        }
        protected override void SetPageToVM()
        {
            try
            {
                InitializeComponent();
            }catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
           
            ((BaseViewModel)BindingContext).Page = this;
        }
        public void SetUserImage(ImageSource source)
        {
            UserImage.Source = source;
        }
    }
}
