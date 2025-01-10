namespace Domain.Entities;

public class Student
{
    public string Name { get; set; }
    public string StudentId { get; set; }
    public List<Course> Courses { get; set; } = new();

    public Student(string name, string studentId)
    {
        Name = name;
        StudentId = studentId;
        Courses = new List<Course>();
    }
}