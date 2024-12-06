namespace Domain.Entities
{
    public class Student
    {
        public Student(string name, string studentId)
        {
            Name = name;
            StudentId = studentId;
            Courses = new List<Course>();
        }

        public string Name { get; private set; }
        public string StudentId { get; private set; }
        public List<Course> Courses { get; private set; }
    }
}