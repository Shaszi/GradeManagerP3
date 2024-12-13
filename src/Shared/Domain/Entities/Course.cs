namespace Domain.Entities
{
    public class Course
    {
        public Course()
        {
            CourseName = string.Empty;
            Credits = 0;
        }

        public Course(string courseName, int credits)
        {
            CourseName = courseName;
            Credits = credits;
        }

        public string CourseName { get; set; }
        public int Credits { get; set; }
        public double Grade { get; set; }
    }
}
