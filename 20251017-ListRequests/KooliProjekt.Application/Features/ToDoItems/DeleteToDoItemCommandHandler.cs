using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class DeleteToDoItemCommandHandler : IRequestHandler<DeleteToDoItemCommand>
    {
        private readonly IToDoItemRepository _repository;

        public DeleteToDoItemCommandHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetAsync(request.Id);

            if (item != null)
            {
                _repository.Remove(item);
                await _repository.SaveChangesAsync();
            }
        }
    }
}