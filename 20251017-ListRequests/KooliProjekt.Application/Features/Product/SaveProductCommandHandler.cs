using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Product
{
    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, int>
    {
        private readonly ApplicationDbContext _context;

        public SaveProductCommandHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            Product product;

            if (request.Id == 0)
            {
                product = new Product();
                _context.Products.Add(product);
            }
            else
            {
                product = await _context.Products
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (product == null)
                {
                    product = new Product();
                    _context.Products.Add(product);
                }
            }

            product.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return product.Id;
        }
    }
}