using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, KooliProjekt.Application.Data.Product?>
    {
        private readonly IProductRepository _repository;

        public GetProductQueryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<KooliProjekt.Application.Data.Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id <= 0)
                return null;

            return await _repository.GetAsync(request.Id);
        }
    }
}