using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Customers;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetCustomerQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrow_WhenRequestIsNull()
        {
            var repo = new Mock<ICustomerRepository>();
            var handler = new GetCustomerQueryHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenIdInvalid()
        {
            var repo = new Mock<ICustomerRepository>();
            var handler = new GetCustomerQueryHandler(repo.Object);

            var result = await handler.Handle(new GetCustomerQuery { Id = 0 }, CancellationToken.None);

            result.Should().BeNull();
            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnCustomer()
        {
            var customer = new Customer { Id = 1, Name = "Test" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(customer);

            var handler = new GetCustomerQueryHandler(repo.Object);

            var result = await handler.Handle(new GetCustomerQuery { Id = 1 }, CancellationToken.None);

            result.Should().NotBeNull();
            result.Id.Should().Be(1);
        }
    }
}