using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MinhaAPI.Controllers;

[ApiController]
[Route("todos")]
public class TodosController : ControllerBase{
    private static readonly List<Todo> Todos = new();
    private static int _nextId = 1;
    // Get com a rota "todos", pega todos os "ToDos"
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<IEnumerable<Todo>> GetAll()
        => Ok(Todos);
    
    [HttpGet("{id:int}")] // Pega por Id
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Todo> GetById(int id){
        var todo = Todos.FirstOrDefault(t=>t.Id ==id);
        return todo is null ? NotFound():Ok(todo);
    }

    // Post com a rota "todos"
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Create([FromBody] CreateTodoDto dto){
        var todo = new Todo(_nextId++,dto.Title.Trim(), dto.Priority,false);
        Todos.Add(todo);
        return Created($"/todos/{todo.Id}",todo);
    }

    [HttpPut("{id:int}")] // atualiza substituindo completamente
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Replace(int id, [FromBody] UpdateTodoDto dto){
        var idx = Todos.FindIndex(t=> t.Id ==id);
        if(idx <0) return NotFound();

        var updated = new Todo(id,dto.Title.Trim(),dto.Priority,dto.Done);
        Todos[idx] = updated;
        return Ok(updated);
    }

    [HttpDelete("{id:int}")] // Remove a task por Id
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id){
        var removed = Todos.RemoveAll(t=>t.Id ==id);
        return removed == 0 ? NotFound():NoContent();
    }
}
public record class CreateTodoDto{
    [Required, MinLength(3)]
    public string Title {get;init;} = string.Empty;
    [Range(0,5)]
    public int Priority{get;init;}
}
public record class UpdateTodoDto{
    [Required, MinLength(3)]
    public string Title {get;init;} = string.Empty;
    [Range(0,5)]
    public int Priority{get;init;}
    public bool Done{get;init;}
}
public record Todo(int Id,string Title, int Priority, bool Done);
