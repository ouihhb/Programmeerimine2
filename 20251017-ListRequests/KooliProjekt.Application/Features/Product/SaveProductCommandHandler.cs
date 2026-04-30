using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, int>
    {
        private readonly IProductRepository _repository;

        public SaveProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var product = await _repository.GetAsync(request.Id);

            if (product == null)
            {
                product = new KooliProjekt.Application.Data.Product();
                await _repository.AddAsync(product);
            }

            product.Name = request.Name;

            await _repository.SaveChangesAsync();

            return product.Id;
        }
    }
}