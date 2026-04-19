using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.ToDoItems;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class GetToDoItemQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repositoryMock = new Mock<IToDoItemRepository>();
            var handler = new GetToDoItemQueryHandler(repositoryMock.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotQueryDatabase_WhenIdIsLessThanOrEqualToZero(int id)
        {
            var repositoryMock = new Mock<IToDoItemRepository>();
            var handler = new GetToDoItemQueryHandler(repositoryMock.Object);

            var request = new GetToDoItemQuery { Id = id };

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().BeNull();
            repositoryMock.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnToDoItem_WhenIdIsValid()
        {
            var item = new ToDoItem
            {
                Id = 1,
                Title = "Test item"
            };

            var repositoryMock = new Mock<IToDoItemRepository>();
            repositoryMock.Setup(x => x.GetAsync(1)).ReturnsAsync(item);

            var handler = new GetToDoItemQueryHandler(repositoryMock.Object);

            var request = new GetToDoItemQuery { Id = 1 };

            var result = await handler.Handle(request, CancellationToken.None);

            result.Should().Be(item);
            repositoryMock.Verify(x => x.GetAsync(1), Times.Once);
        }
    }
}