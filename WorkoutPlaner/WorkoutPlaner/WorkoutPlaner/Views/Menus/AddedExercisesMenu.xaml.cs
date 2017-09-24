using SlideOverKit;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace WorkoutPlaner.Views.Menus
{
    public partial class AddedExerciseMenu : SlideMenuView
    {
        public AddedExerciseMenu(object asd)
        {
            BindingContext = asd;
            InitializeComponent();
            this.IsFullScreen = true;
            this.MenuOrientations = MenuOrientation.BottomToTop;
            this.BackgroundViewColor = Color.Transparent;
            if (Device.OS == TargetPlatform.Android)
                this.HeightRequest += 50;
        }
    }
}