using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola.Data;
using Escola.Models;

namespace Escola.Controllers;

[ApiController]
[Route("api/v1/[controller]")] // Utiliza nome  do arquivo/classe
public class StudentsController : ControllerBase{
    private readonly AppDbContext _db;
    public StudentsController(AppDbContext db)=>_db =db;

    //GET /api/v1/students
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Student>>> GetAll()
        => Ok(await _db.Students.OrderBy(s=>s.Id).ToListAsync());
    
    //GET /api/v1/students/1(id)
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Student>> GetById(int id)
        => await _db.Students.FindAsync(id) is { } s ? Ok(s) : NotFound();

    //POST  /api/v1/students
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Student s){
        if(!string.IsNullOrWhiteSpace(s.Email) &&
            await _db.Students.AnyAsync(x=>x.Email == s.Email)){
                return Conflict(new {error = "Email já cadastrado"});
            }
        _db.Students.Add(s);
        await _db.SaveChangesAsync();
        return CreatedAtAction(nameof (GetById), new {id = s.Id}, s);
    }

    //PUT /api/v1/students/1(id)
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Student s){
        s.Id = id;

         if(!string.IsNullOrWhiteSpace(s.Email) &&
            await _db.Students.AnyAsync(x=>x.Email == s.Email && x.Id != id)){
                return Conflict(new {error = "Email já cadastrado."});
            }

        if(!await _db.Students.AnyAsync(x=> x.Id == id)) return NotFound();

        _db.Entry(s).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return Ok();
    } 

    // DELETE /api/v1/students/1(id)
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete (int id){
        var s = await _db.Students.FindAsync(id);
        if(s is null) return NotFound();

        _db.Students.Remove(s);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}
