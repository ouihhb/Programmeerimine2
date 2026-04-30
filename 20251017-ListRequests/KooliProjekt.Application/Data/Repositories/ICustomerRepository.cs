using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer?> GetAsync(int id);
        Task AddAsync(Customer customer);
        void Remove(Customer customer);
        Task SaveChangesAsync();
    }
}