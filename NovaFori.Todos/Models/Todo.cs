namespace NovaFori.Todos.Models;

public class Todo
{
    public Todo(string description) => Description = description;

    public int Id { get; set; }

    public string Description { get; set; }

    public bool Completed { get; set; }
}