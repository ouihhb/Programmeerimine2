using KooliProjekt.Application.Data;
using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class GetProductQuery : IRequest<KooliProjekt.Application.Data.Product?>
    {
        public int Id { get; set; }
    }
}