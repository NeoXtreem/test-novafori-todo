namespace NovaFori.Todos.Models;

public class Todo
{
    public int Id { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool Completed { get; set; }
}