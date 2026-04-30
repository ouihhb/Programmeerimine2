using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class SaveCustomerCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}