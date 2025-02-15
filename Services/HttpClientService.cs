using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollingSystem.MAUI.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        const string DevTunnelurl = "https://r7h72mcx-7108.inc1.devtunnels.ms/api/";
        const string url = "http://localhost:5053/api/";

        public HttpClientService()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30),
                BaseAddress = new Uri(DevTunnelurl)
            };
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");  // Default Accept header (optional)
        }

        // Generic TryCatch method to handle exceptions and deserialize response
        private async Task<T> TryCatch<T>(Func<Task<HttpResponseMessage>> action)
        {
            try
            {
                var response = await action();

                response.EnsureSuccessStatusCode();
                // Deserialize the response content to the generic type T
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            catch (HttpRequestException ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return default;
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                return default;
            }
        }

        // GET request
        public Task<T> GetAsync<T>(string requestUri)
        {
            return TryCatch<T>(() => _httpClient.GetAsync(requestUri));
        }

        // POST request with JSON data
        public Task<T> PostAsync<T>(string requestUri, object data)
        {
            var jsonData = JsonConvert.SerializeObject(data);  // Convert object to JSON string
            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");  // Prepare the content
            return TryCatch<T>(() => _httpClient.PostAsync(requestUri, content));
        }
    }

}
