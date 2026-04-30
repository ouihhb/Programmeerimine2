using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemQuery : IRequest<OrderItem?>
    {
        public int Id { get; set; }
    }
}