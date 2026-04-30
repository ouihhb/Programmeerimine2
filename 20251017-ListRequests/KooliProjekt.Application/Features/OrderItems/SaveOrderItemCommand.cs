using MediatR;

namespace KooliProjekt.Application.Features.OrderItems
{
    public class SaveOrderItemCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}