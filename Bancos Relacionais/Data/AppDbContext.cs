using Microsoft.EntityFrameworkCore;
using Escola.Models;

namespace Escola.Data;

public class AppDbContext : DbContext{
    public AppDbContext(){}
    public AppDbContext(DbContextOptions<AppDbContext> options) :base(options){}

    public  DbSet<Student> Students => Set<Student>();
    public  DbSet<Course> Courses => Set<Course>();
    public DbSet<StudentCourse> StudentCourses => Set<StudentCourse>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=escola.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Student
        modelBuilder.Entity<Student>(e =>{
            e.HasKey(s=>s.Id);
            e.Property(s=>s.Name).IsRequired().HasMaxLength(120);
            e.Property(s=>s.Email).IsRequired().HasMaxLength(100);
            e.HasIndex(s=>s.Email).IsUnique(); // email único
        });

        //Course
        modelBuilder.Entity<Course>(e =>{
            e.HasKey(c=>c.Id);
            e.Property(c=> c.Name).IsRequired().HasMaxLength(120);
            e.HasIndex(c=>c.Name).IsUnique();
        });

        //StudentCourse (join N:N)
        modelBuilder.Entity<StudentCourse>(e=>{
            e.HasKey(sc=> new {sc.StudentId, sc.CourseId}); // PK Composta
            e.HasOne(sc=> sc.Student)
                .WithMany(s=> s.Enrollments)
                .HasForeignKey(sc=>sc.CourseId)
                .OnDelete(DeleteBehavior.Cascade);

            e.Property(sc=>sc.EnrolledAt).IsRequired();
        });
    }
}

