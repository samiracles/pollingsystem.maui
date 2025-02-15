using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels.Base;
using PollingSystem.MAUI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PollingSystem.MAUI.ViewModels
{
    public class VotePollViewModel : BaseViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private Poll _poll;
        public Poll Poll
        {
            get { return _poll; }
            set { SetProperty(ref _poll, value); }
        }

        private bool _alredyVoted;

        public bool AlreadyVoted
        {
            get { return _alredyVoted; }
            set { SetProperty(ref _alredyVoted, value); }
        }

        public ICommand VoteCommand { get; }
        public ICommand SubmitCommand { get; set; }
        public ICommand ShowResultCommand { get; set; }

        readonly IVoteService _voteService;
        public VotePollViewModel(Poll poll)
        {
            Poll = poll;

            _voteService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IVoteService>();

            VoteCommand = new Command<PollOption>(Vote);
            SubmitCommand = new Command(Submit);
            ShowResultCommand  = new Command(ShowResult);
            CheckIfVoteCasted();
        }

        private async void ShowResult()
        {
            await App.Current.MainPage.Navigation.PushAsync(new PollResultPage(Poll));
        }

        private async void Submit(object obj)
        {
            if (!Poll.Options.Any(x => x.IsChecked))
            {
                return;
            }
            var vote = new Vote
            { 
                PollId = Poll.Id,
                OptionId = Poll.Options.First(x=>x.IsChecked).Id,
                UserId = CacheService.Instance.GetCurrentUser().Id,
                Id = Guid.NewGuid().ToString()
            };
            IsBusy  = true;
            var response = await _voteService.VoteAsync(vote);
            if (response != null && response.Success)
            {
                AlreadyVoted = true;
                CacheService.Instance.AddVote(response.Data);
                await App.Current.MainPage.Navigation.PushAsync(new PollResultPage(Poll));
            }
            IsBusy = false;
        }

        private void Vote(PollOption option)
        {
            if (option != null && !AlreadyVoted)
            {
                Poll.Options.ForEach(x => x.IsChecked = false);
                option.IsChecked = !option.IsChecked;
            }
        }

        async Task CheckIfVoteCasted()
        {
            var vote = new Vote
            {
                Count = 0,
                OptionId = Guid.NewGuid().ToString(),
                Id = Guid.NewGuid().ToString(),
                PollId = Poll.Id,
                UserId = CacheService.Instance.GetCurrentUser().Id,
            };
            IsBusy = true;
            var response = await _voteService.CheckIfVoteCastedAsync(vote);
            if (response != null && response.Success && response.Data != null)
            {
                AlreadyVoted = true;
                Poll.Options.FirstOrDefault(x => x.Id == response.Data.OptionId).IsChecked = true;
            }
            IsBusy = false;
        }
    }
}
