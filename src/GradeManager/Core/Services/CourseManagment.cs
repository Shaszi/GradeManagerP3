using Domain.Entities;
namespace Core.Services
{
    public class CourseManagment
    {
        private readonly List<Course> courses;

        public void AddCourse(Course course)
        {
            courses.Add(course);
        }

        public void RemoveCourse(Course course)
        {
            courses.Remove(course);
        }

        public List<Course> GetAllCourses()
        {
            return courses;
        }

        public Course GetCourseByName(string courseName)
        {
            return courses.Find(c => c.CourseName == courseName);
        }
    }
}
