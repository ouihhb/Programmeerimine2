using KooliProjekt.Application.Data;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class GetProductsQuery : IRequest<PagedResult<KooliProjekt.Application.Data.Product>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}