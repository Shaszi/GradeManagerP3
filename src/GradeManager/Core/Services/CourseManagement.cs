using Domain.Entities;
namespace Core.Services
{
    public class CourseManagement : ICourseManagement
    {
        private readonly IFileService _fileService;

        public CourseManagement(IFileService fileService)
        {
            _fileService = fileService;
        }

        public List<Course> GetAllCourses()
        {
            return _fileService.GetCourses();
        }

        public bool AddCourse(string courseName, int credits)
        {
            try
            {
                var courses = _fileService.GetCourses();
                if (courses.Any(c => c.CourseName == courseName))
                {
                    return false; // Course already exists
                }

                var course = new Course(courseName, credits);
                courses.Add(course);
                _fileService.SaveCourses(courses);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool RemoveCourse(string courseName)
        {
            try
            {
                var courses = _fileService.GetCourses();
                var course = courses.FirstOrDefault(c => c.CourseName == courseName);
                if (course == null)
                {
                    return false; // Course not found
                }

                courses.Remove(course);
                _fileService.SaveCourses(courses);

                // Remove course from all students
                var students = _fileService.GetStudents();
                foreach (var student in students)
                {
                    student.Courses.RemoveAll(c => c.CourseName == courseName);
                }
                _fileService.SaveStudents(students);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}