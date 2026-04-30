using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, Order?>
    {
        private readonly IOrderRepository _repository;

        public GetOrderQueryHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<Order?> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return null;

            return await _repository.GetAsync(request.Id);
        }
    }
}