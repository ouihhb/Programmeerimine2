using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Product;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetProductQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductQueryHandler(repositoryMock.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotQueryDatabase_WhenIdIsLessThanOrEqualToZero(int id)
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductQueryHandler(repositoryMock.Object);

            var request = new GetProductQuery { Id = id };

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeNull();
            repositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnProduct_WhenIdIsValid()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Test product"
            };

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetAsync(1)).ReturnsAsync(product);

            var handler = new GetProductQueryHandler(repositoryMock.Object);

            var request = new GetProductQuery { Id = 1 };

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().Be(product);
            repositoryMock.Verify(x => x.GetAsync(1), Times.Once);
        }
    }
}