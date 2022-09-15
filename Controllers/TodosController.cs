using NovaFori.Todos.Data;
using NovaFori.Todos.Models;
using Microsoft.AspNetCore.Mvc;

namespace NovaFori.Todos.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodoRepository _todoRepository;

    public TodosController(TodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    [HttpGet]
    public IEnumerable<Todo> Get() => _todoRepository.GetTodos();
}