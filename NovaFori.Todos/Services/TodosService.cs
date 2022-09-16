using NovaFori.Todos.Data;
using NovaFori.Todos.Models;

namespace NovaFori.Todos.Services;

public class TodosService
{
    private readonly TodosContext _todosContext;

    public TodosService(TodosContext todosContext)
    {
        _todosContext = todosContext;
    }

    internal IEnumerable<Todo> GetAllTodos() => _todosContext.Todos;

    internal void Add(Todo todo) => _todosContext.Todos.Add(todo);
}
