using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class DeleteCustomerCommand : IRequest
    {
        public int Id { get; set; }
    }
}