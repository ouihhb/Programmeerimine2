using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace KooliProjekt.WpfApplication.Api
{
    public sealed class ApiClient : IApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient() : this(new HttpClient { BaseAddress = new Uri("http://localhost:5086/") })
        {
        }

        public ApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OperationResult<List<Product>>> List()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/Products?pageNumber=1&pageSize=100");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new OperationResult<List<Product>>().AddError(GetApiError(content, response.ReasonPhrase));
                }

                var result = JsonConvert.DeserializeObject<PagedResult<Product>>(content);
                return new OperationResult<List<Product>>(result?.Results?.ToList() ?? new List<Product>());
            }
            catch (Exception ex)
            {
                return new OperationResult<List<Product>>().AddError(ex.Message);
            }
        }

        public async Task<OperationResult<int>> Save(Product product)
        {
            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("api/Products", content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new OperationResult<int>().AddError(GetApiError(responseContent, response.ReasonPhrase));
                }

                return new OperationResult<int>(JsonConvert.DeserializeObject<int>(responseContent));
            }
            catch (Exception ex)
            {
                return new OperationResult<int>().AddError(ex.Message);
            }
        }

        public async Task<OperationResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/Products/{id}");
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new OperationResult().AddError(GetApiError(content, response.ReasonPhrase));
                }

                return new OperationResult();
            }
            catch (Exception ex)
            {
                return new OperationResult().AddError(ex.Message);
            }
        }

        private static string GetApiError(string content, string? fallback)
        {
            return string.IsNullOrWhiteSpace(content) ? fallback ?? "API request failed." : content;
        }
    }
}
