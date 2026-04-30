using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Product;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class DeleteProductCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repo = new Mock<IProductRepository>();
            var handler = new DeleteProductCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotCallRepository_WhenIdInvalid(int id)
        {
            var repo = new Mock<IProductRepository>();
            var handler = new DeleteProductCommandHandler(repo.Object);

            await handler.Handle(new DeleteProductCommand { Id = id }, CancellationToken.None);

            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotRemove_WhenProductNotFound()
        {
            var repo = new Mock<IProductRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync((Product)null);

            var handler = new DeleteProductCommandHandler(repo.Object);

            await handler.Handle(new DeleteProductCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(It.IsAny<Product>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemove_WhenProductExists()
        {
            var product = new Product { Id = 1, Name = "Test" };

            var repo = new Mock<IProductRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(product);

            var handler = new DeleteProductCommandHandler(repo.Object);

            await handler.Handle(new DeleteProductCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(product), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}