using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Orders;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetOrderQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrow_WhenRequestIsNull()
        {
            var repo = new Mock<IOrderRepository>();
            var handler = new GetOrderQueryHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenIdInvalid()
        {
            var repo = new Mock<IOrderRepository>();
            var handler = new GetOrderQueryHandler(repo.Object);

            var result = await handler.Handle(new GetOrderQuery { Id = 0 }, CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnOrder()
        {
            var order = new Order { Id = 1, CustomerId = 1 };

            var repo = new Mock<IOrderRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(order);

            var handler = new GetOrderQueryHandler(repo.Object);

            var result = await handler.Handle(new GetOrderQuery { Id = 1 }, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}