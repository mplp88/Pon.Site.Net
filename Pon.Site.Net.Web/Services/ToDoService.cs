using Microsoft.Extensions.Options;
using Pon.Site.Net.Web.Configuration;
using Pon.Site.Net.Web.Models;
using System.Text.Json;

namespace Pon.Site.Net.Web.Services
{
    public class ToDoService : IToDoService
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<ApiOptions> _apiOptions;
        private JsonSerializerOptions _serializerOptions;

        public ToDoService(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> apiOptions)
        {
            _apiOptions = apiOptions;
            
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(_apiOptions.Value.Url);

            _serializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<Item> Add(Item todo)
        {
            var response = await _httpClient.PostAsJsonAsync(_apiOptions.Value.Endpoints?.ToDo, todo);

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Error guardando datos", new Exception(await response.Content.ReadAsStringAsync()));
            }

            var item = JsonSerializer.Deserialize<Item>(await response.Content.ReadAsStringAsync(), _serializerOptions);
            return item;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync(string.Format("{0}/{1}", _apiOptions.Value.Endpoints?.ToDo, id));

            if(!response.IsSuccessStatusCode)
            {
                throw new Exception("Error guardando datos", new Exception(await response.Content.ReadAsStringAsync()));
            }

            return true;
        }

        public async Task<List<Item>> Get()
        {
            var response = await _httpClient.GetAsync(_apiOptions.Value.Endpoints?.ToDo);
            var items = JsonSerializer.Deserialize<List<Item>>(await response.Content.ReadAsStringAsync(), _serializerOptions);
            return items;
        }

        public async Task<Item?> Get(Guid id)
        {
            var response = await _httpClient.GetAsync(string.Format("{0}/{1}", _apiOptions.Value.Endpoints?.ToDo, id));
            var items = JsonSerializer.Deserialize<Item>(await response.Content.ReadAsStringAsync(), _serializerOptions);
            return items;
        }
    }
}
