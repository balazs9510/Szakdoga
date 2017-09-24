using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using WorkoutPlaner.BLL.Model;
using WorkoutPlaner.BLL.Validations;
using WorkoutPlaner.Common;
using WorkoutPlaner.Common.Enumerations;
using WorkoutPlaner.Common.Keys;
using WorkoutPlaner.DAL.Model;
using WorkoutPlaner.DAL.Seed;
using WorkoutPlaner.Resources;
using WorkoutPlaner.Services;
using WorkoutPlaner.Sync;
using WorkoutPlaner.ViewModels.Base;
using WorkoutPlaner.Views;
using Xamarin.Forms;

namespace WorkoutPlaner.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        #region Properties
        private RegisterModel _registerUser;
        public RegisterModel RegisterUser
        {
            get { return _registerUser; }
            set { SetProperty(ref _registerUser, value); }
        }

        private LoginModel _loginUser;
        public LoginModel LoginUser
        {
            get { return _loginUser; }
            set
            {
                SetProperty(ref _loginUser, value);
                ((Command)LoginCommand).ChangeCanExecute();
            }
        }

        private bool _isLogin;
        public bool IsLogin
        {
            get { return _isLogin; }
            set { SetProperty(ref _isLogin, value); }
        }

        private ObservableCollection<ApplicationUser> _users;
        public ObservableCollection<ApplicationUser> Users
        {
            get { return _users; }
            set { SetProperty(ref _users, value); }
        }

        private ApplicationUser _selectedUser;
        public ApplicationUser SelectedUser
        {
            get { return _selectedUser; }
            set { SetProperty(ref _selectedUser, value); }
        }


        public ICommand RegisterPageCommand => new Command(() => IsLogin = false);
        public ICommand LoginPageCommand => new Command(() => IsLogin = true);
        public ICommand LoginCommand => new Command(() => SignInAsync());
        public ICommand LoginOfflineCommand => new Command(() => LoginOfflineAsync());
        public ICommand RegisterCommand => new Command(() => RegisterAsync());
        public ICommand ValidateLoginEmailCommand => new Command(() => ValidateLoginEmail());
        public ICommand ValidateLoginPasswordCommand => new Command(() => ValidateLoginPassword());
        public ICommand ValidateRegisterEmailCommand => new Command(() => ValidateRegisterEmail());
        public ICommand ValidateRegisterPasswordCommand => new Command(() => ValidateRegisterPassword());
        public ICommand ValidateRegisterConfirmPasswordCommand => new Command(() => ValidateRegisterConfirmPassword());

        public ICommand SelectUserCommand => new Command((o) =>
        {
            SelectedUser = (ApplicationUser)o;
        });

        private IsMatchingPasswordRule<string> _confrimPasswordRule = new IsMatchingPasswordRule<string>() { ValidationMessage = "A két jelszónak meg kell egyeznie" };
        #endregion Properties
        public LoginPageViewModel(INavigationService navigationService, IDatabaseService dbService)
                : base(navigationService, dbService)
        {
            IsLogin = true;
            IsBusy = false;
            LoginUser = new LoginModel();
            RegisterUser = new RegisterModel();
            AddValidations();
            if (!Application.Current.Properties.ContainsKey(StoreKeys.FirstStart))
            {
                Application.Current.Properties[StoreKeys.FirstStart] = false;
                Application.Current.SavePropertiesAsync();
                InitDatabaseAsync();
            }
            Users = new ObservableCollection<ApplicationUser>(_context.Users);
            if(GetValueFromProperties(StoreKeys.RememberMe) is bool value)
            {
                if (value)
                {
                    if(GetValueFromProperties(StoreKeys.LastLoggedInUserId) is string userId)
                    {
                        var lastLoggedInUser = _context.Users.SingleOrDefault(u => u.Id.Equals(userId));
                        LoginUser.Email.Value = lastLoggedInUser.Email;
                        LoginUser.Password.Value = lastLoggedInUser.Password;
                        LoginUser.RememberMe = true;
                    }                  
                }
            }
        }
        private async void InitDatabaseAsync()
        {
            using (var context = new ApplicationContext(DependencyService.Get<IDatabaseService>().GetDatabaseConnectionString()))
            {
                await context.Database.EnsureDeletedAsync();
                await context.Database.EnsureCreatedAsync();

                context.Seed();
            }
        }
        private void AddValidations()
        {
            LoginUser.Email.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Az e-mail mező nem lehet üres" });
            LoginUser.Email.Validations.Add(new IsValidEmailRule<string>() { ValidationMessage = "Nem megfelelő e-mail formátum" });
            LoginUser.Password.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "A jelszó mező nem lehet üres" });
            LoginUser.Password.Validations.Add(new IsValidPasswordRule<string>() { ValidationMessage = "A jelszó minimum 6 karakter és tartalmazni kell egy számot" });
            RegisterUser.Email.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "Az e-mail mező nem lehet üres" });
            RegisterUser.Email.Validations.Add(new IsValidEmailRule<string>() { ValidationMessage = "Nem megfelelő e-mail formátum" });
            RegisterUser.Password.Validations.Add(new IsNotNullOrEmptyRule<string>() { ValidationMessage = "A jelszó mező nem lehet üres" });
            RegisterUser.Password.Validations.Add(new IsValidPasswordRule<string>() { ValidationMessage = "A jelszó minimum 6 karakter és tartalmazni kell egy számot" });
            RegisterUser.ConfirmPassword.Validations.Add(_confrimPasswordRule);
        }
        private async void SignInAsync()
        {
            IsBusy = true;
            if (!ValidateLoginUser())
            {
                await Page.DisplayAlert(AppResources.Error, ValidationMessages.NotValidLoginUser, AppResources.Ok);
            }
            if (!DoIHaveInternet())
            {
                await Page.DisplayAlert(AppResources.Error, AppResources.NoInternet, AppResources.Ok);
            }
            else
            {
                var loginViewModel = new LoginViewModel()
                {
                    Email = LoginUser.Email.Value,
                    Password = LoginUser.Password.Value,
                    RememberMe = LoginUser.RememberMe
                };
                var networkService = new NetworkService();
                try
                {
                    var response = await networkService.PostLogin(loginViewModel);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        SaveValueToPropertiesAsync(StoreKeys.AppMode, (int)ApplicationMode.Online);
                        var message = await response.Content.ReadAsStringAsync();
                        App.AuthCookies = response.Headers.GetValues("Set-Cookie").ToList();
                        
                        ApplicationUser user = JsonConvert.DeserializeObject<ApplicationUser>(message);
                        var existUser = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));
                        if (existUser == null)
                        {
                            user.Password = LoginUser.Password.Value;
                            _context.Users.Add(user);
                        }
                        else
                        {
                            existUser.LastLogin = DateTime.Now;
                            _context.Users.Update(existUser);
                        }
                        try
                        {
                            await _context.SaveChangesAsync();
                            if (LoginUser.RememberMe)
                                SaveValueToPropertiesAsync(StoreKeys.RememberMe, true);
                            else
                                SaveValueToPropertiesAsync(StoreKeys.RememberMe, false);
                            SaveValueToPropertiesAsync(StoreKeys.CurrentUserId,user.Id);
                            SaveValueToPropertiesAsync(StoreKeys.LastLoggedInUserId, user.Id);

                            SyncWithServerAsync(user);
                            NavigateAsync(nameof(MainPage));
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.ToString());
                            await Page.DisplayAlert(
                                AppResources.Error,
                                e.ToString(),
                                AppResources.Ok);
                        }
                    }
                    else
                    {
                        await Page.DisplayAlert(AppResources.Error, AppResources.ConnectionError, AppResources.Ok);
                    }
                }
                catch (Exception e)
                {
                    await Page.DisplayAlert(AppResources.Error,
                        AppResources.ConnectionError + "\n"
                        + e.Message,
                        AppResources.Ok);
                    Debug.WriteLine(e.ToString());
                }
            }
            IsBusy = false;
        }
        private async void RegisterAsync()
        {
            IsBusy = true;

            if (!DoIHaveInternet())
            {
                await Page.DisplayAlert(AppResources.Error, AppResources.NoInternet, AppResources.Ok);
            }
            else
            {
                if (ValidateRegisterUser())
                {
                    var registerVM = new RegisterViewModel()
                    {
                        Age = RegisterUser.Age,
                        Email = RegisterUser.Email.Value,
                        Name = RegisterUser.Name,
                        ConfirmPassword = RegisterUser.ConfirmPassword.Value,
                        Password = RegisterUser.Password.Value,
                        Weigth = RegisterUser.Weigth
                    };
                    try
                    {
                        var networkService = new NetworkService();
                        var response = await networkService.PostRegister(registerVM);
                        var message = await response.Content.ReadAsStringAsync();
                        await Page.DisplayAlert("", message, AppResources.Ok);
                    }
                    catch
                    {
                        await Page.DisplayAlert(AppResources.Error, AppResources.ConnectionError, AppResources.Ok);
                    }
                }
                else
                {
                    await Page.DisplayAlert(AppResources.Error, ValidationMessages.NotValidLoginUser, AppResources.Ok);
                }
            }
            IsBusy = false;
            IsLogin = true;
        }
        private async void LoginOfflineAsync()
        {
            IsBusy = true;
            if (SelectedUser != null)
            {
                Application.Current.Properties[StoreKeys.CurrentUserId] = SelectedUser.Id;
                Application.Current.Properties[StoreKeys.AppMode] = (int)ApplicationMode.Offline;
                MessagingCenter.Send(this, MessagingKeys.ClosePopUp);
                NavigateAsync(nameof(MainPage));
            }
            else
            {
                await Page.DisplayAlert("", AppResources.Choose, AppResources.Ok);
            }

            IsBusy = false;
        }
        private bool ValidateLoginUser()
        {
            var result = false;
            if (LoginUser.Email.Validate() && LoginUser.Password.Validate())
                result = true;
            return result;
        }
        public bool ValidateLoginEmail()
        {
            return LoginUser.Email.Validate();
        }
        public bool ValidateLoginPassword()
        {
            return LoginUser.Password.Validate();
        }
        public bool ValidateRegisterEmail()
        {
            return RegisterUser.Email.Validate();
        }
        public bool ValidateRegisterPassword()
        {
            return RegisterUser.Password.Validate();
        }
        public bool ValidateRegisterConfirmPassword()
        {
            _confrimPasswordRule.Password = RegisterUser.Password.Value;
            return RegisterUser.ConfirmPassword.Validate();
        }
        public bool ValidateRegisterUser()
        {
            return ValidateRegisterEmail() &&
                ValidateRegisterPassword() &&
                ValidateRegisterConfirmPassword();
        }
        private void SyncWithServerAsync(ApplicationUser user)
        {
            CurrentUser = _context.Users.SingleOrDefault(u => u.Id.Equals(user.Id));
            if (CurrentUser.AutSync && DoIHaveInternet()
                            && ((int)GetValueFromProperties(StoreKeys.AppMode) == 0))
            {
                NetworkService ns = new NetworkService() { AuthCookie = GetValueFromProperties(StoreKeys.AuthCookie) as string };
                var syncManager = new SyncManager(_context, CurrentUser.Id,ns);
                syncManager.SyncAsync();
            }
        }
    }
}
