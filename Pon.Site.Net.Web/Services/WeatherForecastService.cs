using Pon.Site.Net.Web.Models;
using System.Text.Json;

namespace Pon.Site.Net.Web.Services
{
    public class WeatherForecastService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public WeatherForecastService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task<List<WeatherForecast>> GetForecast()
        {
            var url = _configuration.GetValue<string>("ExternalServices:PonSiteApi:Url");
            var endpoint = _configuration.GetValue<string>("ExternalServices:PonSiteApi:Endpoints:WeatherForecast");
            _httpClient.BaseAddress = new Uri(url);
            var response = await _httpClient.GetAsync(endpoint);

            if(!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Http error {(int)response.StatusCode}: {content}");
            }

            var forecasts = new List<WeatherForecast>();
            
            try
            {
                if(response.Content is not null)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    forecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(content, new JsonSerializerOptions()
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }

            return forecasts ?? new List<WeatherForecast>();
        }

    }
}
