using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class DeleteOrderItemCommandHandler : IRequestHandler<DeleteOrderItemCommand>
    {
        private readonly IOrderItemRepository _repository;

        public DeleteOrderItemCommandHandler(IOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteOrderItemCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return;

            var item = await _repository.GetAsync(request.Id);

            if (item != null)
            {
                _repository.Remove(item);
                await _repository.SaveChangesAsync();
            }
        }
    }
}