namespace Domain.Entities;

public class Course
{
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public List<decimal> Grades { get; set; } = new();
    public decimal FinalGrade { get; set; }

    public Course(string courseName, int credits)
    {
        CourseName = courseName;
        Credits = credits;
        Grades = new List<decimal>();
    }
}
