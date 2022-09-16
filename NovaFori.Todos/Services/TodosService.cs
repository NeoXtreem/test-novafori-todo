using NovaFori.Todos.Data;
using NovaFori.Todos.Models;

namespace NovaFori.Todos.Services;

public class TodosService
{
    private readonly TodosContext _todosContext;

    public TodosService(TodosContext todosContext) => _todosContext = todosContext;

    internal IEnumerable<Todo> GetAllTodos() => _todosContext.Todos;

    internal async Task<Todo> AddNewTodo(string description)
    {
        var entry = _todosContext.Todos.Add(new Todo(description));
        await _todosContext.SaveChangesAsync();
        return entry.Entity;
    }
}