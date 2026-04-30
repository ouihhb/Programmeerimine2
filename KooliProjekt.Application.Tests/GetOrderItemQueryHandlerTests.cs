using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.OrderItems;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetOrderItemQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrow_WhenRequestIsNull()
        {
            var repo = new Mock<IOrderItemRepository>();
            var handler = new GetOrderItemQueryHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenIdInvalid()
        {
            var repo = new Mock<IOrderItemRepository>();
            var handler = new GetOrderItemQueryHandler(repo.Object);

            var result = await handler.Handle(new GetOrderItemQuery { Id = 0 }, CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task Handle_ShouldReturnItem()
        {
            var item = new OrderItem { Id = 1 };

            var repo = new Mock<IOrderItemRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(item);

            var handler = new GetOrderItemQueryHandler(repo.Object);

            var result = await handler.Handle(new GetOrderItemQuery { Id = 1 }, CancellationToken.None);

            result.Should().NotBeNull();
        }
    }
}