using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.OrderItems;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class DeleteOrderItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repo = new Mock<IOrderItemRepository>();
            var handler = new DeleteOrderItemCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotCallRepository_WhenIdInvalid(int id)
        {
            var repo = new Mock<IOrderItemRepository>();
            var handler = new DeleteOrderItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderItemCommand { Id = id }, CancellationToken.None);

            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotRemove_WhenOrderItemNotFound()
        {
            var repo = new Mock<IOrderItemRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync((OrderItem)null);

            var handler = new DeleteOrderItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderItemCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(It.IsAny<OrderItem>()), Times.Never);
            repo.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemove_WhenOrderItemExists()
        {
            var item = new OrderItem
            {
                Id = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2
            };

            var repo = new Mock<IOrderItemRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(item);

            var handler = new DeleteOrderItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderItemCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(item), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}