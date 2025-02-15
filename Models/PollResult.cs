using PollingSystem.MAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Models
{
    public class PollResult : BaseViewModel
    {
        public string PollId { get; set; }

        private List<VoteCount> _result;

        public List<VoteCount> Result
        {
            get { return _result; }
            set { SetProperty(ref _result, value); }
        }


    }

    public class VoteCount : BaseViewModel
    {
        private string _option;

        public string Option
        {
            get { return _option; }
            set { SetProperty(ref _option, value); }
        }

        private int _count;

        public int Count
        {
            get { return _count; }
            set { SetProperty(ref _count, value); }
        }
    }
}
