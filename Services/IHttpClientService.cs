namespace PollingSystem.MAUI.Services
{
    public interface IHttpClientService
    {
        Task<T> GetAsync<T>(string requestUri);
        Task<T> PostAsync<T>(string requestUri, object data);
    }
}