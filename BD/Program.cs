using Microsoft.EntityFrameworkCore;
using Escola.Models;
using Escola.Data;
using System.Data.Common;

Console.WriteLine("--Escola DB--");
Console.WriteLine("Iniciando/migrando o banco de dados...");

var db = new AppDbContext();
// garante que o banco está criado e com migrações publicadas
await db.Database.MigrateAsync();

var name = "Nelson";
var email = "nelson@gmail.com";

var exists = await db.Students.AnyAsync(s=> s.Email == email);
if(exists){
    Console.WriteLine("Já existe um estudante com este email.");
    return;
}

var student = new Student{
    Name = name,
    Email = email,
    EnrollmentDate = DateTime.UtcNow
};

db.Students.Add(student);
await db.SaveChangesAsync();

Console.WriteLine($"Cadastro realizado com sucesso. Id:{student.Id}");

var lista_estudantes = await db.Students
    .OrderBy(s=> s.Id)
    .ToListAsync();

if(lista_estudantes.Count ==0){
    Console.Write("Nenhum estudante encontrado");
    return;
}

Console.WriteLine("Id | Name                 | Email                    | EnrollmentDate(UTC)");  //Name 17 espaços e Email 20 espaços

foreach(var s in lista_estudantes){
    Console.WriteLine($"{s.Id,2} | {s.Name,-20} | {s.Email,-24} | {s.EnrollmentDate:yyyy-MM-dd HH:mm:ss}");
}
