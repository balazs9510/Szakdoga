using Newtonsoft.Json;
using Plugin.Connectivity;
using Prism.Mvvm;
using Prism.Navigation;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WorkoutPlaner.Common;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.Sync;
using WorkoutPlaner.Views;
using WorkoutPlaner.Views.Popups;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels.Base
{
    public abstract class BaseViewModel : BindableBase, INavigationAware
    {
        /// <summary>Use this property if the page is busy. </summary>
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }
        public Page Page { get; set; }
        protected readonly INavigationService _navigationService;
        protected ApplicationContext _context;
        protected ApplicationUser CurrentUser;
        protected IDatabaseService _dbService;
        public BaseViewModel(INavigationService navigationService, IDatabaseService dbService)
        {
            IsBusy = false;
            _navigationService = navigationService;
            _context = new ApplicationContext(dbService.GetDatabaseConnectionString());
            _dbService = dbService;
            SetUser();
        }
        public async void NavigateAsync(string viewName, NavigationParameters parameters = null)
        {
            await _navigationService.NavigateAsync(viewName, parameters,useModalNavigation:false);
        }
        public virtual void OnNavigatedFrom(NavigationParameters parameters)
        {
            MessagingCenter.Unsubscribe<SyncManager, List<SyncMessage>>(this, MessagingKeys.SyncDone);
        }

        public virtual void OnNavigatedTo(NavigationParameters parameters)
        {
            MessagingCenter.Subscribe<SyncManager, List<SyncMessage>>(this, MessagingKeys.SyncDone, async (s, a) =>
            {
                await Page.Navigation.PushPopupAsync(new SyncResultPopUp(a));
            });
        }

        public virtual void OnNavigatingTo(NavigationParameters parameters)
        {
        }
        protected virtual void SetUser()
        {
            if (Application.Current.Properties.ContainsKey(StoreKeys.CurrentUserId))
            {
                var id = Application.Current.Properties[StoreKeys.CurrentUserId] as string;
                CurrentUser = _context.Users.SingleOrDefault(u => u.Id.Equals(id));
                if (CurrentUser == null)
                    NavigateAsync(nameof(LoginPage));
            }
        }
        public bool DoesKeyExist(string key)
        {
            return Application.Current.Properties.ContainsKey(key);
        }
        public object GetValueFromProperties(string key)
        {
            if (Application.Current.Properties.ContainsKey(key))
            {
                return Application.Current.Properties[key];
            }
            else return null;
        }
        public async void SaveValueToPropertiesAsync(string key, object value)
        {
            Application.Current.Properties[key] = value;
            await Application.Current.SavePropertiesAsync();
        }
        public bool DoIHaveInternet()
        {
            if (!CrossConnectivity.IsSupported)
                return true;

            //Do this only if you need to and aren't listening to any other events as they will not fire.
            using (var connectivity = CrossConnectivity.Current)
            {
                return connectivity.IsConnected;
            }
        }
        protected async void DisplaySimpleMessageAsync(string message)
        {
            await Page.DisplayAlert("", message, AppResources.Ok);
        }
        public async Task<T> GetObjectFromResponseAsync<T>(HttpResponseMessage response)
        {
            string json = string.Empty;
            json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
