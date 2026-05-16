using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace KooliProjekt.WindowsForms.Api
{
    public sealed class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri("http://localhost:5086/");
        }

        public async Task<List<Product>> GetProducts()
        {
            var result = await _httpClient.GetFromJsonAsync<PagedResult<Product>>("api/Products?pageNumber=1&pageSize=100");
            return result?.Results?.ToList() ?? new List<Product>();
        }
    }
}
