using KooliProjekt.Application.Data;

namespace KooliProjekt.Application.Data.Repositories
{
    public class ToDoItemRepository : BaseRepository<ToDoItem>, IToDoItemRepository
    {
        public ToDoItemRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}