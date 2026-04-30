using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Product;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class SaveProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrow_WhenRequestNull()
        {
            var repo = new Mock<IProductRepository>();
            var handler = new SaveProductCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Handle_ShouldCreateNewProduct()
        {
            var repo = new Mock<IProductRepository>();
            repo.Setup(x => x.GetAsync(0)).ReturnsAsync((Product)null);

            var handler = new SaveProductCommandHandler(repo.Object);

            var id = await handler.Handle(new SaveProductCommand { Id = 0, Name = "Test" }, CancellationToken.None);

            repo.Verify(x => x.AddAsync(It.IsAny<Product>()), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}