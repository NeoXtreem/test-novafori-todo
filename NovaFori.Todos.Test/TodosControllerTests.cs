using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Moq;
using NovaFori.Todos.Controllers;
using NovaFori.Todos.Data;
using NovaFori.Todos.Models;

namespace NovaFori.Todos.Test
{
    [TestClass]
    public class TodosControllerTests
    {
        private readonly Fixture _fixture = new();

        [TestMethod]
        public void EmptyDatabaseShouldReturnNoTodos()
        {
            // Arrange
            var data = Array.Empty<Todo>().AsQueryable();
            var mockContext = GetMockContext(GetMockSet(data));

            // Action
            var todos = GetSut(mockContext).Get();

            // Assert
            todos.Should().BeEmpty();
        }

        [TestMethod]
        public void NonEmptyDatabaseShouldReturnAllTodos()
        {
            // Arrange
            var data = _fixture.CreateMany<Todo>().AsQueryable();
            var mockContext = GetMockContext(GetMockSet(data));

            // Action
            var todos = GetSut(mockContext).Get();

            // Assert
            todos.Should().Equal(data);
        }

        [TestMethod]
        public async Task AddingTodoShouldReturnOneAdditionalTodo()
        {
            // Arrange
            var data = _fixture.CreateMany<Todo>().AsQueryable();
            var mockSet = GetMockSet(data);
            var mockContext = GetMockContext(mockSet);
            var description = _fixture.Create<string>();
            mockSet.Setup(m => m.Add(It.IsAny<Todo>())).Returns(GetMockEntityEntry().Object);

            // Action
            var todo = await GetSut(mockContext).Post(description);

            // Assert
            mockSet.Verify(m => m.Add(It.IsAny<Todo>()), Times.Once());
            mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        private static TodosController GetSut(Mock<TodosContext> mockContext)
        {
            return new TodosController(new(mockContext.Object));
        }

        private Mock<TodosContext> GetMockContext(Mock<DbSet<Todo>> mockSet)
        {
            var mockContext = new Mock<TodosContext>(_fixture.Create<DbContextOptions<TodosContext>>());
            mockContext.SetupGet(m => m.Todos).Returns(mockSet.Object);
            return mockContext;
        }

        private static Mock<DbSet<Todo>> GetMockSet(IEnumerable<Todo> data)
        {
            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            return mockSet;
        }

        private static Mock<EntityEntry<Todo>> GetMockEntityEntry()
        {
            // TODO: This is a workaround to resolve an issue in EF Core where Add returns the entity entry and not the entry. See: https://github.com/dotnet/efcore/issues/27110#issuecomment-1009000699
            var internalEntityEntry = new InternalEntityEntry(
                new Mock<IStateManager>().Object,
                new RuntimeEntityType("T", typeof(Todo), false, null, null, null, ChangeTrackingStrategy.Snapshot, null, false),
                null);

            return new Mock<EntityEntry<Todo>>(internalEntityEntry);
        }
    }
}