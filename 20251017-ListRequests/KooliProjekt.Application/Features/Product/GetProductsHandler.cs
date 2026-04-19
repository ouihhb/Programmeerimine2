using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, PagedResult<KooliProjekt.Application.Data.Product>>
    {
        private readonly IProductRepository _repository;
        private const int MaxPageSize = 100;

        public GetProductsHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<KooliProjekt.Application.Data.Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.PageNumber <= 0)
                throw new ArgumentException("PageNumber must be greater than 0.");

            if (request.PageSize <= 0)
                throw new ArgumentException("PageSize must be greater than 0.");

            if (request.PageSize > MaxPageSize)
                throw new ArgumentException($"PageSize must not be greater than {MaxPageSize}.");

            var all = await _repository.GetAllAsync();

            return new PagedResult<KooliProjekt.Application.Data.Product>
            {
                Results = all.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList(),
                PageSize = request.PageSize
            };
        }
    }
}