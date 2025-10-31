using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Escola.Data;
using Escola.Models;

var builder = WebApplication.CreateBuilder(args);

// Porta fixa (opcional, facilita testes)
builder.WebHost.UseUrls("http://localhost:5099");

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=escola.db"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

var webTask = app.RunAsync();
Console.WriteLine("API online em http://localhost:5099 (Swagger em /swagger)");

Console.WriteLine("== SchoolDbLab ==");
Console.WriteLine("Console + API executando juntos!");

while (true)
{
    Console.WriteLine();
    Console.WriteLine("Escolha uma opção:");
    Console.WriteLine("1 - Cadastrar estudante");
    Console.WriteLine("2 - Listar estudantes");
    Console.WriteLine("3 - Atualizar estudante (por Id)");
    Console.WriteLine("4 - Remover estudante (por Id)");
    Console.WriteLine("5 - Cadastrar Curso");
    Console.WriteLine("6 - Matricular aluno em curso");
    Console.WriteLine("7 - Listas CURSOS e ALUNOS");
    Console.WriteLine("8 - LISTAR ALUNOS por CURSO");
    Console.WriteLine("0 - Sair");
    Console.Write("> ");

    var opt = Console.ReadLine();

    if (opt == "0") break;

    switch (opt)
    {
        case "1": await CreateStudentAsync(); break;
        case "2": await ListStudentsAsync(); break;
        case "3": await UpdateStudentAsync(); break;
        case "4": await DeleteStudentAsync(); break;
        case "5": await CreateCourseAsync(); break;
        case "6": await EnrollStudentInCourseAsync(); break;
        case "7": await ListCoursesWithStudentsAsync(); break; //INNER JOIN com foco em curso
        case "8": await ListStudentsByCourseAsync(); break; //INNER JOIN com foco em estudante
        default: Console.WriteLine("Opção inválida."); break; 
    }
}

await app.StopAsync();
await webTask;

async Task CreateStudentAsync()
{
    Console.Write("Nome: ");
    var name = (Console.ReadLine() ?? "").Trim();

    Console.Write("Email: ");
    var email = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

    if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
    {
        Console.WriteLine("Nome e Email são obrigatórios.");
        return;
    }

    using var db = new AppDbContext();
    var exists = await db.Students.AnyAsync(s => s.Email == email);
    if (exists) { Console.WriteLine("Já existe um estudante com esse email."); return; }

    var student = new Student { Name = name, Email = email, EnrollmentDate = DateTime.UtcNow };
    db.Students.Add(student);
    await db.SaveChangesAsync();
    Console.WriteLine($"Cadastrado com sucesso! Id: {student.Id}");
}

async Task ListStudentsAsync()
{
    using var db = new AppDbContext();
    var students = await db.Students.OrderBy(s => s.Id).ToListAsync();

    if (students.Count == 0) { Console.WriteLine("Nenhum estudante encontrado."); return; }

    Console.WriteLine("Id | Name                 | Email                    | EnrollmentDate (UTC)");
    foreach (var s in students)
        Console.WriteLine($"{s.Id,2} | {s.Name,-20} | {s.Email,-24} | {s.EnrollmentDate:yyyy-MM-dd HH:mm:ss}");
}

async Task UpdateStudentAsync()
{
    Console.Write("Informe o Id do estudante a atualizar: ");
    if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();
    var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
    if (student is null) { Console.WriteLine("Estudante não encontrado."); return; }

    Console.WriteLine($"Atualizando Id {student.Id}. Deixe em branco para manter.");
    Console.WriteLine($"Nome atual : {student.Name}");
    Console.Write("Novo nome  : ");
    var newName = (Console.ReadLine() ?? "").Trim();

    Console.WriteLine($"Email atual: {student.Email}");
    Console.Write("Novo email : ");
    var newEmail = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

    if (!string.IsNullOrWhiteSpace(newName)) student.Name = newName;
    if (!string.IsNullOrWhiteSpace(newEmail))
    {
        var emailTaken = await db.Students.AnyAsync(s => s.Email == newEmail && s.Id != id);
        if (emailTaken) { Console.WriteLine("Já existe outro estudante com esse email."); return; }
        student.Email = newEmail;
    }

    await db.SaveChangesAsync();
    Console.WriteLine("Estudante atualizado com sucesso.");
}

async Task DeleteStudentAsync()
{
    Console.Write("Informe o Id do estudante a remover: ");
    if (!int.TryParse(Console.ReadLine(), out var id)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();
    var student = await db.Students.FirstOrDefaultAsync(s => s.Id == id);
    if (student is null) { Console.WriteLine("Estudante não encontrado."); return; }

    db.Students.Remove(student);
    await db.SaveChangesAsync();
    Console.WriteLine("Estudante removido com sucesso.");
}

async Task CreateCourseAsync()
{
    Console.Write("Nome do curso: ");
    var name = (Console.ReadLine() ?? "").Trim();

    if (string.IsNullOrWhiteSpace(name)) { Console.WriteLine("Nome é obrigatório."); return; }

    using var db = new AppDbContext();
    if (await db.Courses.AnyAsync(c => c.Name == name))
    {
        Console.WriteLine("Curso já existe.");
        return;
    }

    var course = new Course { Name = name };
    db.Courses.Add(course);
    await db.SaveChangesAsync();
    Console.WriteLine($"Curso criado! Id: {course.Id}");
}

async Task EnrollStudentInCourseAsync()
{
    Console.Write("Id do aluno: ");
    if (!int.TryParse(Console.ReadLine(), out var sid)) { Console.WriteLine("Id inválido."); return; }

    Console.Write("Id do curso: ");
    if (!int.TryParse(Console.ReadLine(), out var cid)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();

    // Mostra qual arquivo de banco está sendo usado (diagnóstico)
    Console.WriteLine("DB Path: " + Path.GetFullPath("escola.db"));

    // Verifica existência real no banco
    var student = await db.Students.AsNoTracking().FirstOrDefaultAsync(s => s.Id == sid);
    var course  = await db.Courses.AsNoTracking().FirstOrDefaultAsync(c => c.Id == cid);
    Console.WriteLine($"Exists? Student({sid})={(student!=null)}, Course({cid})={(course!=null)}");

    if (student is null || course is null)
    {
        Console.WriteLine("Aluno ou curso não encontrado. Operação cancelada.");
        return;
    }

    // Evita duplicidade
    var already = await db.StudentCourses.AnyAsync(sc => sc.StudentId == sid && sc.CourseId == cid);
    if (already)
    {
        Console.WriteLine("Aluno já está matriculado neste curso.");
        return;
    }

    // Se quiser, “anexe” entidades para garantir tracking correto (não insere aluno/curso de novo)
    db.Attach(new Student { Id = sid });
    db.Attach(new Course  { Id = cid  });

    db.StudentCourses.Add(new StudentCourse { StudentId = sid, CourseId = cid });

    try
    {
        await db.SaveChangesAsync();
        Console.WriteLine($"Matriculado com sucesso: {student.Name} -> {course.Name}");
    }
    catch (DbUpdateException ex)
    {
        Console.WriteLine("Falha ao salvar matrícula.");
        Console.WriteLine("Inner: " + (ex.InnerException?.Message ?? ex.Message));
    }
}

async Task ListCoursesWithStudentsAsync()
{
    using var db = new AppDbContext();

    // INNER JOIN explícito (apenas cursos COM alunos)
    var rows = await (
        from c  in db.Courses
        join sc in db.StudentCourses on c.Id equals sc.CourseId
        join s  in db.Students       on sc.StudentId equals s.Id
        orderby c.Name, s.Name
        select new { Course = c.Name, Student = s.Name, s.Email }
    ).ToListAsync();

    if (rows.Count == 0) { Console.WriteLine("Nenhuma matrícula encontrada (INNER JOIN)."); return; }

    string? current = null;
    foreach (var r in rows)
    {
        if (current != r.Course)
        {
            current = r.Course;
            Console.WriteLine($"\nCurso: {current}");
            Console.WriteLine("  Alunos:");
        }
        Console.WriteLine($"    - {r.Student} ({r.Email})");
    }
    Console.WriteLine();
}

async Task ListStudentsByCourseAsync()
{
    Console.Write("Id do curso: ");
    if (!int.TryParse(Console.ReadLine(), out var cid)) { Console.WriteLine("Id inválido."); return; }

    using var db = new AppDbContext();

    var course = await db.Courses.FindAsync(cid);
    if (course is null) { Console.WriteLine("Curso não encontrado."); return; }

    var students = await (
        from sc in db.StudentCourses
        join s  in db.Students on sc.StudentId equals s.Id
        where sc.CourseId == cid
        orderby s.Name
        select new { s.Name, s.Email }
    ).ToListAsync();

    Console.WriteLine($"\nCurso: {course.Name}");
    if (students.Count == 0) { Console.WriteLine("  (sem alunos)"); return; }

    foreach (var st in students)
        Console.WriteLine($"  - {st.Name} ({st.Email})");
}
