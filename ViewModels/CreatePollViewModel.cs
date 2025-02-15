using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PollingSystem.MAUI.ViewModels
{
    public class CreatePollViewModel : BaseViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        private string _question;
        public string Question
        {
            get { return _question; }
            set { SetProperty(ref _question, value); }
        }

        private DateTime _startDae;

        public DateTime StartDate
        {
            get { return _startDae; }
            set { _startDae = value; }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }



        private ObservableCollection<PollOption> _options;
        public ObservableCollection<PollOption> Options
        {
            get { return _options; }
            set { SetProperty(ref _options, value); }
        }

        public ICommand AddOptionCommand { get; }
        public ICommand RemoveOptionCommand { get; }
        public ICommand CreatePollCommand { get; }

        private readonly IPollService _pollService;
        public CreatePollViewModel()
        {
            _pollService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IPollService>();
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(7);
            Options = new ObservableCollection<PollOption>() { new PollOption { Index = 1, Id = Guid.NewGuid().ToString() } };
            AddOptionCommand = new Command(AddOption);
            RemoveOptionCommand = new Command<PollOption>(RemoveOption);
            CreatePollCommand = new Command(CreatePoll);
        }

        private void AddOption()
        {
            if (Options?.Count <=3)
            {
                Options.Add(new PollOption { Index = Options.Count + 1, Id = Guid.NewGuid().ToString() });
            }
        }

        private void RemoveOption(PollOption option)
        {
            if (Options.Count <= 1) return;
            if (Options.Contains(option))
            {
                Options.Remove(option);

                for (int i = 0; i < Options.Count; i++)
                {
                    Options[i].Index = i + 1;
                }
            }
        }

        private async void CreatePoll()
        {
            if (string.IsNullOrEmpty(Question) || Options.Count <= 0)
                return;

            if (Options.Any(x => string.IsNullOrEmpty(x.Option)))
                return;

            var poll = new Poll
            {  
                Id = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.Now,
                Question = Question,
                Options = Options.ToList(),
                StartDate = StartDate,
                EndDate = EndDate,
                UserId = CacheService.Instance.GetCurrentUser().Id
            };

            foreach (var item in poll.Options)
            {
                item.PollId = poll.Id;
            }
            IsBusy = true;  
            var result = await _pollService.SavePollAsync(poll);
            if (result != null)
            {
                CacheService.Instance.AddPoll(poll);
                App.Current.MainPage.Navigation.PopAsync();
            }
            IsBusy = false;
        }
    }
}
