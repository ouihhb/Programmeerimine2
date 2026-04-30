using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQuery : IRequest<Order?>
    {
        public int Id { get; set; }
    }
}