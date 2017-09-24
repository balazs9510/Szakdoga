
using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Practices.Unity;
using Plugin.Media;
using Prism.Unity;
using WorkoutPlaner.Droid.Services;
using WorkoutPlaner.Services;

namespace WorkoutPlaner.Droid
{
    [Activity(Label = "WorkoutPlaner", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override async void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            await CrossMedia.Current.Initialize();
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IDatabaseService, DatabaseService>();
            container.RegisterType<ILocalize, Localize>();
        }
    }
}

