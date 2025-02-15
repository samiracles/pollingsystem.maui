using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Models
{
    public class Vote
    {
        public string Id { get; set; }
        public string PollId { get; set; }
        public string OptionId { get; set; }
        public string UserId { get; set; }
        public int Count { get; set; }
    }
}
