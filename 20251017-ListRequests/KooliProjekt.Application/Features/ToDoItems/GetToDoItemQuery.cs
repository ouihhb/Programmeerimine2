using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.ToDoItems
{
    public class GetToDoItemQuery : IRequest<ToDoItem?>
    {
        public int Id { get; set; }
    }
}