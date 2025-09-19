using Microsoft.EntityFrameworkCore;
using Escola.Data;
using Escola.Models;
using System.Data.Common;

Console.WriteLine("--Escola DB--");
Console.WriteLine("Inicializando/migrando o banco...");

var db = new AppDbContext();
// garante que o banco estáa criado e com migrações publicadas
await db.Database.MigrateAsync();

var name = "b";
var email = "b@gmail.com";

// Verificação simples para evitar duplicidade de email
var exists = await db.Students.AnyAsync(s=> s.Email == email);
if(exists){
    Console.WriteLine("Já existe um estudante com este email");
    return;
}
var student = new Student{
    Name = name,
    Email = email,
    EnrollmentDate = DateTime.UtcNow
};

db.Students.Add(student);
await db.SaveChangesAsync();

Console.WriteLine($"Cadastro com sucesso! Id: {student.Id}");

var lista_estudantes = await db.Students
    .OrderBy(s=> s.Id)
    .ToListAsync();

if(lista_estudantes.Count == 0){
    Console.WriteLine("Nenhum estudante encontrado.");
    return;
}

foreach(var s in lista_estudantes){
    Console.WriteLine($"{s.Id,2} | {s.Name,-20} | {s.Email,-24} | {s.EnrollmentDate:yyyy-MM-dd HH:mm:ss}");
}
