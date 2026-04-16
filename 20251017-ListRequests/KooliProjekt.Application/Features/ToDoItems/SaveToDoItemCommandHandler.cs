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
            var toDoItem = await _repository.GetAsync(request.Id);

            if (toDoItem == null)
            {
                toDoItem = new ToDoItem();
                await _repository.AddAsync(toDoItem);
            }

            toDoItem.Title = request.Name;

            await _repository.SaveChangesAsync();

            return toDoItem.Id;
        }
    }
}