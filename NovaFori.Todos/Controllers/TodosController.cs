using Microsoft.AspNetCore.Mvc;
using NovaFori.Todos.Models;
using NovaFori.Todos.Services;

namespace NovaFori.Todos.Controllers;

[ApiController]
[Route("[controller]")]
public class TodosController : ControllerBase
{
    private readonly TodosService _todosService;

    public TodosController(TodosService todosService)
    {
        _todosService = todosService;
    }

    [HttpGet]
    public IEnumerable<Todo> Get() => _todosService.GetAllTodos();

    [HttpPost]
    public void Post([FromBody] Todo todo) => _todosService.Add(todo);
}