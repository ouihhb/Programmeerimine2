using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Customers;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class DeleteCustomerCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repo = new Mock<ICustomerRepository>();
            var handler = new DeleteCustomerCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotCallRepository_WhenIdInvalid(int id)
        {
            var repo = new Mock<ICustomerRepository>();
            var handler = new DeleteCustomerCommandHandler(repo.Object);

            await handler.Handle(new DeleteCustomerCommand { Id = id }, CancellationToken.None);

            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotRemove_WhenCustomerNotFound()
        {
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync((Customer)null);

            var handler = new DeleteCustomerCommandHandler(repo.Object);

            await handler.Handle(new DeleteCustomerCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(It.IsAny<Customer>()), Times.Never);
            repo.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemove_WhenCustomerExists()
        {
            var customer = new Customer { Id = 1, Name = "Test", Email = "test@test.ee" };

            var repo = new Mock<ICustomerRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(customer);

            var handler = new DeleteCustomerCommandHandler(repo.Object);

            await handler.Handle(new DeleteCustomerCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(customer), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}