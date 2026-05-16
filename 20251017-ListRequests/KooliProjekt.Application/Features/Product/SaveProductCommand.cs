using MediatR;

namespace KooliProjekt.Application.Features.Product
{
    public class SaveProductCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
