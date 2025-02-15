using PollingSystem.MAUI.Models;

namespace PollingSystem.MAUI.Services
{
    public interface IUserService
    {
        Task<Response<User>> SignInAsync(User user);
        Task<Response<User>> SignUpAsync(User user);
    }
}