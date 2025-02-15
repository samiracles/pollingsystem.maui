using PollingSystem.MAUI.Models;
using PollingSystem.MAUI.Services;
using PollingSystem.MAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.ViewModels
{
    public class PollResultViewModel : BaseViewModel
    {
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public Poll Poll { get; }

        private PollResult _pollResult;

        public PollResult PollResult
        {
            get { return _pollResult; }
            set { SetProperty(ref _pollResult, value); }

        }


        readonly IPollService _pollService;

        public PollResultViewModel(Poll poll)
        {
            _pollService = Application.Current.Windows[0].Page.Handler.MauiContext.Services.GetService<IPollService>();
            Poll = poll;
            GetVoteCount();
        }

        private async void GetVoteCount()
        {
            IsBusy = true;
            var response = await _pollService.GetPollResultAsync(Poll.Id);
            if (response!= null && response.Success && response.Data != null)
            {
                PollResult = response.Data;
            }
            IsBusy = false;
        }
    }
}
