using System;
using System.Threading;
using System.Threading.Tasks;
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
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return null;

            return await _repository.GetAsync(request.Id);
        }
    }
}