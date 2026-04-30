using FluentAssertions;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Data.Repositories;
using KooliProjekt.Application.Features.ToDoItems;
using Moq;
using Xunit;

namespace KooliProjekt.Application.Tests
{
    public class DeleteToDoItemCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            var repo = new Mock<IToDoItemRepository>();
            var handler = new DeleteToDoItemCommandHandler(repo.Object);

            Func<Task> act = async () => await handler.Handle(null, CancellationToken.None);

            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task Handle_ShouldNotCallRepository_WhenIdInvalid(int id)
        {
            var repo = new Mock<IToDoItemRepository>();
            var handler = new DeleteToDoItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteToDoItemCommand { Id = id }, CancellationToken.None);

            repo.Verify(x => x.GetAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldNotRemove_WhenItemNotFound()
        {
            var repo = new Mock<IToDoItemRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync((ToDoItem)null);

            var handler = new DeleteToDoItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteToDoItemCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(It.IsAny<ToDoItem>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldRemove_WhenItemExists()
        {
            var item = new ToDoItem { Id = 1, Title = "Test" };

            var repo = new Mock<IToDoItemRepository>();
            repo.Setup(x => x.GetAsync(1)).ReturnsAsync(item);

            var handler = new DeleteToDoItemCommandHandler(repo.Object);

            await handler.Handle(new DeleteToDoItemCommand { Id = 1 }, CancellationToken.None);

            repo.Verify(x => x.Remove(item), Times.Once);
            repo.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}