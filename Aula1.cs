using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Controllers;

[ApiController]
[Route("todos")] // rota comum para GET e POST
public class TodosController : ControllerBase{
    private static readonly List<Todo> Todos = new();
    private static int _nextId = 1;

    [HttpGet] //Lista todos os Todos(tasks)
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]    
    public ActionResult<IEnumerable<Todo>> GetAll() => Ok(Todos);

    [HttpGet("{id:int}")] //Pega Todo(task) por ID
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]    
    public ActionResult<Todo> GetById(int id){
        var todo = Todos.FirstOrDefault(t =>t.Id == id);
        return todo is null ? NotFound() : Ok(todo);
    }

    [HttpPost] // Cria
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateTodoDto dto){
        var todo = new Todo(_nextId++, dto.Title.Trim(),dto.Priority,false);
        Todos.Add(todo);
        return Created($"/todos/{todo.Id}", todo);
    }
}
public record class CreateTodoDto{
    [Required, MinLength(3)]
    public string Title{get;init;} = string.Empty;
    [Range(0,5)]
    public int Priority {get;init;}
}
public record Todo(int Id, string Title, int Priority, bool Done);
