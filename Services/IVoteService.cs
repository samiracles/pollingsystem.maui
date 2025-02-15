using PollingSystem.MAUI.Models;

namespace PollingSystem.MAUI.Services
{
    public interface IVoteService
    {
        Task<Response<Vote>> VoteAsync(Vote vote);
        Task<Response<Vote>> CheckIfVoteCastedAsync(Vote vote);
    }
}