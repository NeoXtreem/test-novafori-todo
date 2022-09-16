using AutoFixture;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NovaFori.Todos.Controllers;
using NovaFori.Todos.Data;
using NovaFori.Todos.Models;

namespace NovaFori.Todos.Test
{
    [TestClass]
    public class TodoRepositoryTests
    {
        private readonly Fixture _fixture = new();

        [TestMethod]
        public void EmptyDatabaseShouldReturnNoTodos()
        {
            // Arrange
            var data = Array.Empty<Todo>().AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = BuildContext(mockSet);

            var sut = new TodosController(new(mockContext.Object));

            // Action
            var todos = sut.Get().ToArray();

            // Assert
            todos.Should().BeEmpty();
        }

        [TestMethod]
        public void GetAllTodos()
        {
            // Arrange
            var data = _fixture.CreateMany<Todo>().AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());
            var mockContext = BuildContext(mockSet);

            var sut = new TodosController(new(mockContext.Object));

            // Action
            var todos = sut.Get().ToArray();

            // Assert
            todos.Should().Equal(data);
        }

        private Mock<TodosContext> BuildContext(IMock<DbSet<Todo>> mockSet)
        {
            var options = _fixture.Create<DbContextOptions<TodosContext>>();
            var mockContext = new Mock<TodosContext>(options);
            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);
            return mockContext;
        }
    }
}