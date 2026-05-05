using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQuery : IRequest<PagedResult<Order>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? CustomerId { get; set; }
    }
}