using PollingSystem.MAUI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Models
{
    public class PollOption : BaseViewModel
    {
        public string Id { get; set; }

        private int _index;
        public int Index
        {
            get { return _index; }
            set { SetProperty(ref _index, value); }

        }
        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }

        }

        public string PollId { get; set; }
        public string Option { get; set; }
    }
}
