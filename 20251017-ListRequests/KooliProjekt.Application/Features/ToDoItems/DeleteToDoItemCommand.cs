using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class DeleteToDoItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}