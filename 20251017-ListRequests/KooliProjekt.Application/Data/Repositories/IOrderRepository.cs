using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync();
        Task<Order?> GetAsync(int id);
        Task AddAsync(Order order);
        void Remove(Order order);
        Task SaveChangesAsync();
    }
}