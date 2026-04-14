using KooliProjekt.Application.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Product
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Product?>
    {
        private readonly ApplicationDbContext _context;

        public GetProductQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return await _context.Products
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}