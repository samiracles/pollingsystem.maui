using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Models
{
    public class Poll
    {
        public string Id { get; set; }
        public string Question { get; set; }
        public List<PollOption> Options { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
