using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IOrderItemRepository
    {
        Task<List<OrderItem>> GetAllAsync();
        Task<OrderItem?> GetAsync(int id);
        Task AddAsync(OrderItem item);
        void Remove(OrderItem item);
        Task SaveChangesAsync();
    }
}