using System.Collections.Generic;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product?> GetAsync(int id);
        Task AddAsync(Product product);
        void Remove(Product product);
        Task SaveChangesAsync();
    }
}