using System;
using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Orders;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class DeleteOrderCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repo = new Mock<IOrderRepository>();
            var handler = new DeleteOrderCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotCallRepository_WhenIdInvalid(int id)
        {
            var repo = new Mock<IOrderRepository>();
            var handler = new DeleteOrderCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderCommand { Id = id }, CancellationToken.None);

            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotRemove_WhenOrderNotFound()
        {
            var repo = new Mock<IOrderRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync((Order)null);

            var handler = new DeleteOrderCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(It.IsAny<Order>()), Times.Never);
            repo.Verify(x => x.SaveChangesAsync(), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemove_WhenOrderExists()
        {
            var order = new Order
            {
                Id = 1,
                CustomerId = 1,
                OrderDate = DateTime.Now
            };

            var repo = new Mock<IOrderRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(order);

            var handler = new DeleteOrderCommandHandler(repo.Object);

            await handler.Handle(new DeleteOrderCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(order), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}