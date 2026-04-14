using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class SaveToDoItemCommandHandler : IRequestHandler<SaveToDoItemCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public SaveToDoItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(SaveToDoItemCommand request, CancellationToken cancellationToken)
        {
            ToDoItem toDoItem;

            if (request.Id == 0)
            {
                toDoItem = new ToDoItem();
                _context.ToDoItems.Add(toDoItem);
            }
            else
            {
                toDoItem = await _context.ToDoItems
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (toDoItem == null)
                {
                    toDoItem = new ToDoItem();
                    _context.ToDoItems.Add(toDoItem);
                }
            }

            toDoItem.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return toDoItem.Id;
        }
    }
}