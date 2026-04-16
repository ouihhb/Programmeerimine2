using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
    {
        private readonly ApplicationDbContext _context;

        public DeleteToDoItemCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.ToDoItems
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (item != null)
            {
                _context.ToDoItems.Remove(item);
                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}