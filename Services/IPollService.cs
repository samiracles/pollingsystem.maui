using PollingSystem.MAUI.Models;

namespace PollingSystem.MAUI.Services
{
    public interface IPollService
    {
        Task<Response<List<Poll>>> GetPollsAsync();
        Task<Response<Poll>> SavePollAsync(Poll poll);
        Task<Response<PollResult>> GetPollResultAsync(string pollId);
    }
}