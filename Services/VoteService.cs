using PollingSystem.MAUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Services
{
    public class VoteService : IVoteService
    {
        private readonly IHttpClientService _httpClientService;
        readonly string controller = "Vote";
        public VoteService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<Response<Vote>> VoteAsync(Vote vote)
        {
            return await _httpClientService.PostAsync<Response<Vote>>(controller, vote);
        }

        public async Task<Response<Vote>> CheckIfVoteCastedAsync(Vote vote)
        {
            var url = $"{controller}/checkIfVoteCasted";
            return await _httpClientService.PostAsync<Response<Vote>>(url, vote);
        }

    }
}
