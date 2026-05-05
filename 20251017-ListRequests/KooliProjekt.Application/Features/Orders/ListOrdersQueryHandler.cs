using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, PagedResult<Order>>
    {
        private readonly IOrderRepository _repository;
        private const int MaxPageSize = 100;

        public ListOrdersQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
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

            if (request.CustomerId.HasValue)
            {
                all = all.Where(x => x.CustomerId == request.CustomerId.Value).ToList();
            }

            return new PagedResult<Order>
            {
                Results = all.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList(),
                PageSize = request.PageSize
            };
        }
    }
}