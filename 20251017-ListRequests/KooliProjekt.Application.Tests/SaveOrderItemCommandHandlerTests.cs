using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.OrderItems;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class SaveOrderItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldCreateItem()
        {
            var repo = new Mock<IOrderItemRepository>();
            repo.Setup(x => x.GetAsync(0)).ReturnsAsync((OrderItem)null);

            var handler = new SaveOrderItemCommandHandler(repo.Object);

            var id = await handler.Handle(new SaveOrderItemCommand { Id = 0, OrderId = 1 }, CancellationToken.None);

            repo.Verify(x => x.AddAsync(It.IsAny<OrderItem>()), Times.Once);
        }
    }
}