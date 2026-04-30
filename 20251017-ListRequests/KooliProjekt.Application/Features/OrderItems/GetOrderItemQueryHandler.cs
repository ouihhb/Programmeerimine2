using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class GetOrderItemQueryHandler : IRequestHandler<GetOrderItemQuery, OrderItem?>
    {
        private readonly IOrderItemRepository _repository;

        public GetOrderItemQueryHandler(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrderItem?> Handle(GetOrderItemQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return null;

            return await _repository.GetAsync(request.Id);
        }
    }
}