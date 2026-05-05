using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class ListOrderItemsQuery : IRequest<PagedResult<OrderItem>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? OrderId { get; set; }
        public int? ProductId { get; set; }
    }
}