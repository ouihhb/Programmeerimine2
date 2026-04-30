using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }
    }
}