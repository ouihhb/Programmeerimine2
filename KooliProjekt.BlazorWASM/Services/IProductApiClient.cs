using KooliProjekt.BlazorWASM.Models;

namespace KooliProjekt.BlazorWASM.Services
{
    public interface IProductApiClient
    {
        Task<List<Product>> List();
        Task<Product> Get(int id);
        Task Save(Product product);
    }
}
