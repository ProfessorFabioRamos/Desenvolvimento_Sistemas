namespace Escola.Models;

public class Course{
    public int Id{get;set;}
    public string Name {get;set;} = "";

    public List<StudentCourse> Enrollments{get;set;} = new();
} 
