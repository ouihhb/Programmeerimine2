using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, Customer?>
    {
        private readonly ICustomerRepository _repository;

        public GetCustomerQueryHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer?> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return null;

            return await _repository.GetAsync(request.Id);
        }
    }
}