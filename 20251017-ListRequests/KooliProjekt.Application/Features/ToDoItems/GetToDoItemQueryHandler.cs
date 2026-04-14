using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, ToDoItem?>
    {
        private readonly ApplicationDbContext _context;

        public GetToDoItemQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ToDoItem?> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            return await _context.ToDoItems
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}