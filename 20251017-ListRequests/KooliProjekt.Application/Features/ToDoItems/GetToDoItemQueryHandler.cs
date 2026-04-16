using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQueryHandler : IRequestHandler<GetToDoItemQuery, ToDoItem?>
    {
        private readonly IToDoItemRepository _repository;

        public GetToDoItemQueryHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<ToDoItem?> Handle(GetToDoItemQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id);
        }
    }
}