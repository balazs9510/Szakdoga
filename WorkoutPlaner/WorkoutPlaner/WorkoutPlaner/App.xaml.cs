using Microsoft.EntityFrameworkCore;
using Prism.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.DAL.Seed;
using WorkoutPlaner.Services;
using WorkoutPlaner.Views;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner
{

    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        public static List<string> AuthCookies { get; set; }
        protected override void OnInitialized()
        {

            AuthCookies = new List<string>();
            InitializeComponent();
//#if DEBUG
//            NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(MainPage));
//#else
       NavigationService.NavigateAsync(nameof(NavigationPage) + "/" + nameof(LoginPage));
//#endif
        }

        protected override void RegisterTypes()
        {

            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<CustomNavigationPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<WorkoutsPage>();
            Container.RegisterTypeForNavigation<MuscleGroupPage>();
            Container.RegisterTypeForNavigation<HistoryPage>();
            Container.RegisterTypeForNavigation<ProfilPage>();
            Container.RegisterTypeForNavigation<CreateWorkoutPage>();
            Container.RegisterTypeForNavigation<ExerciseSerialRepsPopup>();
            Container.RegisterTypeForNavigation<WorkoutDetailPage>();
            Container.RegisterTypeForNavigation<CompleteWorkoutPage>();
        }

        
    }

}
