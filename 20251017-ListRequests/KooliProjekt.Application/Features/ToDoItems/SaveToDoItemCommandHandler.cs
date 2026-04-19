using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class SaveToDoItemCommandHandler : IRequestHandler<SaveToDoItemCommand, int>
    {
        private readonly IToDoItemRepository _repository;

        public SaveToDoItemCommandHandler(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveToDoItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetAsync(request.Id);

            if (item == null)
            {
                item = new ToDoItem();
                await _repository.AddAsync(item);
            }

            item.Title = request.Name;

            await _repository.SaveChangesAsync();

            return item.Id;
        }
    }
}