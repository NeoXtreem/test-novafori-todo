using Microsoft.EntityFrameworkCore;
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

    internal async Task<Todo> SetTodo(int id, bool completed)
    {
        var todo = _todosContext.Todos.ToArray().Single(t => t.Id == id);
        todo.Completed = completed;
        var entry = _todosContext.Todos.Update(todo);
        await _todosContext.SaveChangesAsync();
        return entry.Entity;
    }
}