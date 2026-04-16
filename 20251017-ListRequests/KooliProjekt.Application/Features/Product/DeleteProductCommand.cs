using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }
    }
}