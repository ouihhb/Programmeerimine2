using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.Product;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetProductsHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductsHandler(repositoryMock.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldThrowArgumentException_WhenPageNumberIsInvalid(int pageNumber)
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductsHandler(repositoryMock.Object);

            var request = new GetProductsQuery
            {
                PageNumber = pageNumber,
                PageSize = 10
            };

            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeIsInvalid(int pageSize)
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductsHandler(repositoryMock.Object);

            var request = new GetProductsQuery
            {
                PageNumber = 1,
                PageSize = pageSize
            };

            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Handle_ShouldThrowArgumentException_WhenPageSizeTooLarge()
        {
            var repositoryMock = new Mock<IProductRepository>();
            var handler = new GetProductsHandler(repositoryMock.Object);

            var request = new GetProductsQuery
            {
                PageNumber = 1,
                PageSize = 101
            };

            Func<Task> act = async () => await handler.Handle(request, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Fact]
        public async Task Handle_ShouldReturnPagedResult_WhenValid()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "P1" },
                new Product { Id = 2, Name = "P2" },
                new Product { Id = 3, Name = "P3" }
            };

            var repositoryMock = new Mock<IProductRepository>();
            repositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(products);

            var handler = new GetProductsHandler(repositoryMock.Object);

            var request = new GetProductsQuery
            {
                PageNumber = 1,
                PageSize = 2
            };

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().NotBeNull();
            result.Results.Should().HaveCount(2);

            repositoryMock.Verify(x => x.GetAllAsync(), Times.Once);
        }
    }
}