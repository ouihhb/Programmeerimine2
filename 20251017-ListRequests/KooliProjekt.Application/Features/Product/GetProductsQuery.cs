using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

public class GetProductsQuery : IRequest<PagedResult<Product>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}