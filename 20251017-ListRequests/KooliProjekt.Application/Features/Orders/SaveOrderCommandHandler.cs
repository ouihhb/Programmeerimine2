using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class SaveOrderCommandHandler : IRequestHandler<SaveOrderCommand, int>
    {
        private readonly IOrderRepository _repository;

        public SaveOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetAsync(request.Id);

            if (order == null)
            {
                order = new Order();
                await _repository.AddAsync(order);
            }

            order.CustomerId = request.CustomerId;
            order.OrderDate = request.OrderDate;

            await _repository.SaveChangesAsync();

            return order.Id;
        }
    }
}