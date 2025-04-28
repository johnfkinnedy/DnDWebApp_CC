using DnDWebApp_CC.Models.Entities;

namespace DnDWebApp_CC.Services
{
    public class APIService
    {
        private readonly HttpClient _httpClient;

        public APIService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7130/api/");

        }

        public async Task<IEnumerable<Background>> GetBackgroundsAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Background>>("background/all");
        }
    }
}
