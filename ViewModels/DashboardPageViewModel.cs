using PollingSystem.MAUI.Helper;
using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels.Base;
using PollingSystem.MAUI.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PollingSystem.MAUI.ViewModels
{
    public class DashboardPageViewModel : BaseViewModel
    {
        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private ObservableCollection<Poll> _activePolls;
        public ObservableCollection<Poll> ActivePolls
        {
            get { return _activePolls; }
            set { SetProperty(ref _activePolls, value); }
        }

        private ObservableCollection<Poll> _ownPolls;
        public ObservableCollection<Poll> OwnPolls
        {
            get { return _ownPolls; }
            set { SetProperty(ref _ownPolls, value); }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public bool IsGuest { get; set; }

        public ICommand NavigateToCreatePollCommand { get; }
        public ICommand PollSelectedCommand { get; }
        public ICommand LogoutCommand { get; }
        public ICommand RefreshCommand { get; }

        readonly IPollService _pollService;
        public DashboardPageViewModel()
        {
            Title = $"Dashboard - { CacheService.Instance.GetCurrentUser().Username}";
            _pollService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IPollService>();

            NavigateToCreatePollCommand = new Command(NavigateToCreatePoll);
            PollSelectedCommand = new Command<Poll>(NavigateToVotePoll);
            LogoutCommand = new Command(LogoutAsync);
            RefreshCommand = new Command(LoadPolls);
            IsGuest = CacheService.Instance.GetCurrentUser().Username == AppSettings.Guest;
            LoadPolls();
        }

        private void LogoutAsync()
        {
            CacheService.Instance.Clear();
            App.Current.MainPage = new LoginPage();
        }

        private async void NavigateToCreatePoll()
        {
            await App.Current.MainPage.Navigation.PushAsync(new CreatePollPage());
        }

        private async void NavigateToVotePoll(Poll poll)
        {
            if (poll != null)
            {
                if (poll.UserId == CacheService.Instance.GetCurrentUser().Id)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new PollResultPage(poll));
                }
                else
                {
                    bool isBetween = DateTime.Now >= poll.StartDate && DateTime.Now <= poll.EndDate;
                    if (!isBetween)
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new PollResultPage(poll));
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new VotePollPage(poll));
                    }
                }
            }
        }

        private async void LoadPolls()
        {
            IsBusy = true;
            var response = await _pollService.GetPollsAsync();
            if (response != null && response.Success && response.Data.Count > 0)
            {
                OwnPolls = new ObservableCollection<Poll>(response.Data.Where(x => x.UserId == CacheService.Instance.GetCurrentUser().Id));
                ActivePolls = new ObservableCollection<Poll>(response.Data.Where(x => x.UserId != CacheService.Instance.GetCurrentUser().Id));
            }
            IsBusy = false;
        }
    }
}
