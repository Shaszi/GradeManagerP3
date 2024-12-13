namespace Domain.Entities
{
    public class Course
    {
        public Course()
        {
            CourseName = string.Empty;
            Credits = 0;
            Grades = new List<decimal>();
        }

        public Course(string courseName, int credits)
        {
            CourseName = courseName;
            Credits = credits;
            Grades = new List<decimal>();
        }

        public string CourseName { get; set; }
        public int Credits { get; set; }
        public List<decimal> Grades { get; set; }
        public decimal FinalGrade => Grades.Any() ? Grades.Average() : 0;
    }
}
