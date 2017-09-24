using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Navigation;
using System.Diagnostics;
using System.Windows.Input;
using WorkoutPlaner.Common;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class ProfilPageViewModel : BaseViewModel
    {
        private bool _takePicEnded;
        public bool TakePicEnabled
        {
            get { return _takePicEnded; }
            set
            {
                ((Command)TakePhotoCommand).ChangeCanExecute();
                SetProperty(ref _takePicEnded, value);
            }
        }
        private ApplicationUser _user;
        public ApplicationUser User
        {
            get { return _user; }
            set
            {
                SetProperty(ref _user, value);
                _context.Users.Update(User);
            }
        }

        public ICommand TakePhotoCommand => new Command(TakePhotoAsync, (o) => TakePicEnabled);

        private async void TakePhotoAsync(object obj)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                if (Page != null)
                    await Page.DisplayAlert(AppResources.NoCam, AppResources.NoCamerAvailable, AppResources.Ok);
                return;
            }
            var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());
            if (file == null)
            {
                if (Page != null)
                    await Page.DisplayAlert(AppResources.Error, AppResources.SaveFileError, AppResources.Ok);
                return;
            }
            if (Page != null)
            {
                await Page.DisplayAlert(" ", AppResources.FilePlace + " " + file.Path, AppResources.Ok);
                ((ProfilPage)Page).SetUserImage(ImageSource.FromFile(file.Path));
            }
            User.PhotoPath = file.Path;
            _context.Users.Update(User);
            _context.SaveChanges();
            file.Dispose();
        }

        public ProfilPageViewModel(INavigationService navigationService, IDatabaseService dbService)
            : base(navigationService, dbService)
        {
            TakePicEnabled = true;
            User = CurrentUser;
            MessagingCenter.Subscribe<ApplicationUser>(this, MessagingKeys.UserChanged, (s) =>
             {
                 _context.Users.Update(User);
                 _context.SaveChanges();
             });
        }
        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Unsubscribe<ApplicationUser>(this, MessagingKeys.UserChanged);
        }
        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            MessagingCenter.Subscribe<ApplicationUser>(this, MessagingKeys.UserChanged, async (s) =>
             {
                 _context.Users.Update(User);
                 await _context.SaveChangesAsync();
             });
        }
    }
}

