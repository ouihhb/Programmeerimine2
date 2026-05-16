using System.Net.Http.Json;
using KooliProjekt.BlazorWASM.Models;

namespace KooliProjekt.BlazorWASM.Services
{
    public class ProductApiClient : IProductApiClient
    {
        private readonly HttpClient _httpClient;

        public ProductApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Product>> List()
        {
            var result = await _httpClient.GetFromJsonAsync<PagedResult<Product>>("api/Products?pageNumber=1&pageSize=100");
            return result?.Results?.ToList() ?? new List<Product>();
        }

        public async Task<Product> Get(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Products/{id}") ?? new Product();
        }

        public async Task Save(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Products", product);
            response.EnsureSuccessStatusCode();
        }
    }
}
