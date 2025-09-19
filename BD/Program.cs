using Microsoft.EntityFrameworkCore;
using Escola.Data;
using Escola.Models;

Console.WriteLine("--Escola DB--");
Console.WriteLine("Inicializando/migrando o banco...");

var db = new AppDbContext();
// garante que o banco estáa criado e com migrações publicadas
await db.Database.MigrateAsync();

var name = "Maria";
var email = "maria@gmail.com";

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
