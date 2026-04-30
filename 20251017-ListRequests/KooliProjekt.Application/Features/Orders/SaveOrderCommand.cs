using System;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class SaveOrderCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}