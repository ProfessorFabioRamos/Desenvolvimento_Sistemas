namespace Escola.Models;

public class Course{
    public int Id{get;set;}
    public string Name {get;set;} = "";

    public string Description{get;set;} = "";

    public List<StudentCourse> StudentCourses{get;set;} = new();
} 
