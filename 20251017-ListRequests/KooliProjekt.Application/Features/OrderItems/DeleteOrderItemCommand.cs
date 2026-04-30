using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class DeleteOrderItemCommand : IRequest
    {
        public int Id { get; set; }
    }
}