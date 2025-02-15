using PollingSystem.MAUI.Models;

namespace PollingSystem.MAUI.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpClientService _httpClientService;
        readonly string controller = "User";
        public UserService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<Response<User>> SignUpAsync(User user)
        {
            var url = $"{controller}/create";
            return await _httpClientService.PostAsync<Response<User>>(url, user);
        }

        public async Task<Response<User>> SignInAsync(User user)
        {
            var url = $"{controller}/login";
            return await _httpClientService.PostAsync<Response<User>>(url, user);
        }
    }
}
