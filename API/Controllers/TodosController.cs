using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinhaPrimeiraApi.Controllers;

[ApiController]
[Route("todos")] // base path comum
public class TodosController : ControllerBase
{
    private static readonly List<Todo> Todos = new();
    private static int _nextId = 1;

    // GET /todos  -> lista todos
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<Todo>> GetAll()
        => Ok(Todos);

    // POST /todos -> cria
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateTodoDto dto)
    {
        // [ApiController] já valida DataAnnotations e devolve 400 se inválido
        var todo = new Todo(_nextId++, dto.Title.Trim(), dto.Priority, false);
        Todos.Add(todo);
        return Created($"/todos/{todo.Id}", todo);
    }
}

public record class CreateTodoDto
{
    [Required, MinLength(3)]
    public string Title { get; init; } = string.Empty;

    [Range(0, 5)]
    public int Priority { get; init; }
}

public record Todo(int Id, string Title, int Priority, bool Done);
