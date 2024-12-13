using Domain.Entities;

namespace Core.Services
{
    public class ManagerService
    {
        private readonly DataService _dataService;
        private readonly CourseManagementService _courseService;
        private readonly StudentManagementService _studentService;
        private readonly GradeManagementService _gradeService;

        public List<Student> Students => _dataService.Students;
        public List<Course> AvailableCourses => _dataService.AvailableCourses;

        public ManagerService()
        {
            _dataService = new DataService();
            _courseService = new CourseManagementService(_dataService);
            _studentService = new StudentManagementService(_dataService);
            _gradeService = new GradeManagementService(_dataService, _courseService);
            _dataService.LoadFromJson();
        }

        public void ManageCourses() => _courseService.ManageCourses();
        public void ManageStudents() => _studentService.ManageStudents();
        public void ManageStudentCourses(Student student) => _gradeService.ManageStudentCourses(student);
    }
}
