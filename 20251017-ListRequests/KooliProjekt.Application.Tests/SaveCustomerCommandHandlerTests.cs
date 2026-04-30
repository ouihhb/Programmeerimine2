using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Customers;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class SaveCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateCustomer()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(x => x.GetAsync(0)).ReturnsAsync((Customer)null);

            var handler = new SaveCustomerCommandHandler(repo.Object);

            var id = await handler.Handle(new SaveCustomerCommand { Id = 0, Name = "Test" }, CancellationToken.None);

            repo.Verify(x => x.AddAsync(It.IsAny<Customer>()), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}