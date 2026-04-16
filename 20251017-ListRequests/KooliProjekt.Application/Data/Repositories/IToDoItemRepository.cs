using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Data.Repositories
{
    public interface IToDoItemRepository
    {
        Task<List<ToDoItem>> GetAllAsync();
        Task<ToDoItem?> GetAsync(int id);
        Task AddAsync(ToDoItem item);
        void Remove(ToDoItem item);
        Task SaveChangesAsync();
    }
}