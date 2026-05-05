using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class SaveOrderItemCommandHandler : IRequestHandler<SaveOrderItemCommand, int>
    {
        private readonly IOrderItemRepository _repository;

        public SaveOrderItemCommandHandler(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveOrderItemCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var item = await _repository.GetAsync(request.Id);

            if (item == null)
            {
                item = new OrderItem();
                await _repository.AddAsync(item);
            }

            item.OrderId = request.OrderId;
            item.ProductId = request.ProductId;
            item.Quantity = request.Quantity;

            await _repository.SaveChangesAsync();

            return item.Id;
        }
    }
}