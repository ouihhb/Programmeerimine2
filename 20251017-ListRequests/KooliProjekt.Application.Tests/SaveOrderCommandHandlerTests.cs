using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Orders;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class SaveOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateOrder()
        {
            var repo = new Mock<IOrderRepository>();
            repo.Setup(x => x.GetAsync(0)).ReturnsAsync((Order)null);

            var handler = new SaveOrderCommandHandler(repo.Object);

            var id = await handler.Handle(new SaveOrderCommand { Id = 0, CustomerId = 1 }, CancellationToken.None);

            repo.Verify(x => x.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}