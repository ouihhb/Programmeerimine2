using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Infrastructure.Paging;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomersQueryHandler : IRequestHandler<ListCustomersQuery, PagedResult<Customer>>
    {
        private readonly ICustomerRepository _repository;
        private const int MaxPageSize = 100;

        public ListCustomersQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<PagedResult<Customer>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
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

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                all = all
                    .Where(x =>
                        (x.Name != null && x.Name.Contains(request.Search, StringComparison.OrdinalIgnoreCase)) ||
                        (x.Email != null && x.Email.Contains(request.Search, StringComparison.OrdinalIgnoreCase)))
                    .ToList();
            }

            return new PagedResult<Customer>
            {
                Results = all.Skip((request.PageNumber - 1) * request.PageSize)
                             .Take(request.PageSize)
                             .ToList(),
                PageSize = request.PageSize
            };
        }
    }
}