namespace Domain.Entities
{
    public class Student
    {
        public Student()
        {
            Name = string.Empty;
            StudentId = string.Empty;
            Courses = new List<Course>();
        }

        public Student(string name, string studentId)
        {
            Name = name;
            StudentId = studentId;
            Courses = new List<Course>();
        }

        public string Name { get; set; }
        public string StudentId { get; set; }
        public List<Course> Courses { get; set; }
    }
}