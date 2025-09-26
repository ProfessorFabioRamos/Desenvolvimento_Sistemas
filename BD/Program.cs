using Microsoft.EntityFrameworkCore;
using Escola.Data;
using Escola.Models;
using System.Data.Common;

Console.WriteLine("--Escola DB--");
Console.WriteLine("Inicializando/migrando o banco...");

using(var db = new AppDbContext()){
    // garante que o banco está criado e com migrações publicadas
    await db.Database.MigrateAsync();
}

while(true){
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Cadastrar Estudante");
    Console.WriteLine("2 - Listar Estudantes");
    Console.WriteLine("3 - Atualizar Estudante (por ID)");
    Console.WriteLine("4 - Remover Estudante (por ID)");
    Console.WriteLine("0 - Sair");
    Console.WriteLine(">");

    var option = Console.ReadLine();
    if(option == "0") break;

    switch(option){
        case "1":
            await CreateStudentAsync();
            break;
        case "2":
            await ListStudentAsync();
            break;
        case "3":
            await UpdateStudentAsync();
            break;
        case "4":
            await DeleteStudentAsync();
            break;
        default:
            Console.WriteLine("Opção Inválida.");
            break;
    }
}

async Task CreateStudentAsync(){
    Console.Write("Nome: ");
    var name = Console.ReadLine();

    Console.Write("Email: ");
    var email = Console.ReadLine();

    if(string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email)){
        Console.WriteLine("Nome e email são obrigatórios.");
        return;
    }

    using var db = new AppDbContext();

    // Verificação simples para evitar duplicidade de email
    var exists = await db.Students.AnyAsync(s=> s.Email == email);
    if(exists){
        Console.WriteLine("Já existe um estudante com este email");
        return;
    }
    var student = new Student{
        Name = name.Trim(),
        Email = email.Trim().ToLowerInvariant(),
        EnrollmentDate = DateTime.UtcNow
    };

    db.Students.Add(student);
    await db.SaveChangesAsync();

    Console.WriteLine($"Cadastro com sucesso! Id: {student.Id}");
}

async Task ListStudentAsync(){
    using var db = new AppDbContext();

    var lista_estudantes = await db.Students
        .OrderBy(s=> s.Id)
        .ToListAsync();

    if(lista_estudantes.Count == 0){
        Console.WriteLine("Nenhum estudante encontrado.");
        return;
    }

    Console.WriteLine("Id | Name                 | Email                    | EnrollmentDate(UTC)"); //Depois do name 17 espaços, depois do email 20 espaços
    foreach(var s in lista_estudantes){
        Console.WriteLine($"{s.Id,2} | {s.Name,-20} | {s.Email,-24} | {s.EnrollmentDate:yyyy-MM-dd HH:mm:ss}");
    }
}

async Task UpdateStudentAsync(){
    Console.Write("Informe o Id do estudante a atualizar:");
    if(!int.TryParse(Console.ReadLine(), out var id)){
        Console.WriteLine("Id inválido.");
        return;
    }

    using var db = new AppDbContext();

    var student = await db.Students.FirstOrDefaultAsync(s=>s.Id ==id);
    if (student is null){
        Console.WriteLine("Estudante não encontrado.");
        return;
    }
    Console.WriteLine($"Atualizando Id {student.Id}");
    Console.WriteLine($"Nome atual: {student.Name}");
    Console.Write("Novo nome: ");
    var newName = Console.ReadLine();

    Console.WriteLine($"Email atual: {student.Email}");
    Console.Write("Novo email: ");
    var newEmail = Console.ReadLine();

    if(!string.IsNullOrWhiteSpace(newName)) student.Name = newName;
    if(!string.IsNullOrWhiteSpace(newEmail)){
        var emailTaken = await db.Students.AnyAsync(s=>s.Email == newEmail && s.Id != id);
        if(emailTaken){
            Console.WriteLine("Já existe outro estudante com esse email.");
            return;
        }
        student.Email = newEmail;
    }
    await db.SaveChangesAsync();
    Console.WriteLine("Estudante atualizado com sucesso!");
}

async Task DeleteStudentAsync()
{
    Console.Write("Informe o Id do estudante a remover: ");
    if (!int.TryParse(Console.ReadLine(), out var id))
    {
        Console.WriteLine("Id inválido.");
        return;
    }

    using var db = new AppDbContext();

    var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
    if (student is null)
    {
        Console.WriteLine("Estudante não encontrado.");
        return;
    }

    db.Students.Remove(student);
    await db.SaveChangesAsync();
    Console.WriteLine("Estudante removido com sucesso.");
}
