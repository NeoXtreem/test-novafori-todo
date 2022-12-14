using NovaFori.Todos.Models;
using Microsoft.EntityFrameworkCore;

namespace NovaFori.Todos.Data;

public class TodosContext : DbContext
{
    public virtual DbSet<Todo> Todos { get; set; }

    public TodosContext(DbContextOptions<TodosContext> options) : base(options)
    {
    }
}