using PollingSystem.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Services
{
    public class PollService : IPollService
    {
        private readonly IHttpClientService _httpClientService;
        readonly string controller = "Poll";
        public PollService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<Response<List<Poll>>> GetPollsAsync()
        {
            return await _httpClientService.GetAsync<Response<List<Poll>>>(controller);
        }

        public async Task<Response<Poll>> SavePollAsync(Poll poll)
        {
            return await _httpClientService.PostAsync<Response<Poll>>(controller, poll);
        }

        public async Task<Response<PollResult>> GetPollResultAsync(string pollId)
        {
            var url = $"{controller}/{pollId}/results";
            return await _httpClientService.GetAsync<Response<PollResult>>(url);
        }


    }
}