using System;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class SaveCustomerCommandHandler : IRequestHandler<SaveCustomerCommand, int>
    {
        private readonly ICustomerRepository _repository;

        public SaveCustomerCommandHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
        {

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var customer = await _repository.GetAsync(request.Id);

            if (customer == null)
            {
                customer = new Customer();
                await _repository.AddAsync(customer);
            }

            customer.Name = request.Name;
            customer.Email = request.Email;

            await _repository.SaveChangesAsync();

            return customer.Id;
        }
    }
}