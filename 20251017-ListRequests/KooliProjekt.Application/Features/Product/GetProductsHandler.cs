using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<Product>>
{
    private readonly IProductRepository _repository;

    public GetProductsHandler(IProductRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var pageNumber = request.PageNumber <= 0 ? 1 : request.PageNumber;
        var pageSize = request.PageSize <= 0 ? 10 : request.PageSize;

        var all = await _repository.GetAllAsync();

        return new PagedResult<Product>
        {
            Results = all.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList(),
            TotalCount = all.Count,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }
}