using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class SaveToDoItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}