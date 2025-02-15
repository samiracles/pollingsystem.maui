using PollingSystem.MAUI.Helper;
using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels.Base;
using PollingSystem.MAUI.Views;
using System.Windows.Input;

namespace PollingSystem.MAUI.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get { return _username; }
            set { SetProperty(ref _username, value); }
        }

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set { SetProperty(ref _isAdmin, value); }
        }



        public ICommand SignInCommand { get; }
        public ICommand SignUpCommand { get; }
        public ICommand GuestSignInCommand { get; }
        public ICommand IsAdminCommand { get; }

        public LoginViewModel()
        {
            SignUpCommand = new Command(SignUpAsync);
            SignInCommand = new Command(SignInAsync);
            GuestSignInCommand = new Command(GuestSignUpAsync);
            IsAdminCommand = new Command(() => { IsAdmin = !IsAdmin; });
        }

        private async void GuestSignUpAsync()
        {
            IsBusy = true;
            var guid = Guid.NewGuid();
            var user = new User
            {
                Id =  $"{guid}",
                Username = $"{AppSettings.Guest}{guid}",
                IsAdmin = false


            };
            var userService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IUserService>();
            var result = await userService.SignUpAsync(user);
            IsBusy = false;
            if (result != null && result.Success)
            {
                CacheService.Instance.SetCurrentUser(new User { Id = user.Id, Username = AppSettings.Guest });
                App.Current.MainPage = new NavigationPage(new DashboardPage());
            }
        }

        private async void SignInAsync()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                IsBusy = true;
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = Username,
                    IsAdmin = false
                };
                var userService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IUserService>();
                var result = await userService.SignInAsync(user);
                if (result != null && result.Success)
                {

                    CacheService.Instance.SetCurrentUser(result.Data);
                    App.Current.MainPage = new NavigationPage(new DashboardPage());
                }
                IsBusy = false;
            }
        }

        private async void SignUpAsync()
        {
            if (!string.IsNullOrEmpty(Username))
            {
                IsBusy = true;
                var user = new User
                {
                    Id = Guid.NewGuid().ToString(),
                    Username = Username,
                    IsAdmin = false
                };
                var userService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IUserService>();
                var result = await userService.SignUpAsync(user);
                if (result != null && result.Success)
                {
                    CacheService.Instance.SetCurrentUser(result.Data);
                    App.Current.MainPage = new NavigationPage(new DashboardPage());
                }
                IsBusy = false;
            }
        }
    }
}
