using NovaFori.Todos.Models;

namespace NovaFori.Todos.Data;

public class TodoRepository
{
    private readonly TodosContext _context;

    public TodoRepository(TodosContext context) => _context = context;

    public IEnumerable<Todo> GetTodos() => _context.Todos;
}