using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class ListOrderItemsQueryHandler : IRequestHandler<ListOrderItemsQuery, PagedResult<OrderItem>>
    {
        private readonly IOrderItemRepository _repository;
        private const int MaxPageSize = 100;

        public ListOrderItemsQueryHandler(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<OrderItem>> Handle(ListOrderItemsQuery request, CancellationToken cancellationToken)
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

            if (request.OrderId.HasValue)
                all = all.Where(x => x.OrderId == request.OrderId.Value).ToList();

            if (request.ProductId.HasValue)
                all = all.Where(x => x.ProductId == request.ProductId.Value).ToList();

            return new PagedResult<OrderItem>
            {
                Results = all.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList(),
                PageSize = request.PageSize
            };
        }
    }
}