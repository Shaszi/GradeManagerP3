using Domain.Entities;

namespace Core.Services
{
    public class GradeManagementService
    {
        private readonly DataService _dataService;
        private readonly CourseManagementService _courseService;

        public GradeManagementService(DataService dataService, CourseManagementService courseService)
        {
            _dataService = dataService;
            _courseService = courseService;
        }

        public void ManageStudentCourses(Student student)
        {
            while (true)
            {
                Console.WriteLine($"\nManaging courses for {student.Name}");
                Console.WriteLine("1. Add Course from Available Courses");
                Console.WriteLine("2. Add Grade");
                Console.WriteLine("3. Return to Main Menu");
                
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddCourseToStudent(student);
                        break;
                    case "2":
                        AddGrade(student);
                        break;
                    case "3":
                        return;
                }
            }
        }

        private void AddCourseToStudent(Student student)
        {
            if (_dataService.AvailableCourses.Count == 0)
            {
                Console.WriteLine("No available courses. Please add courses first.");
                return;
            }

            _courseService.ListAvailableCourses();
            Console.Write("\nEnter the course number to add: ");
            if (int.TryParse(Console.ReadLine(), out var courseIndex) && 
                courseIndex > 0 && courseIndex <= _dataService.AvailableCourses.Count)
            {
                var selectedCourse = _dataService.AvailableCourses[courseIndex - 1];
                var courseCopy = new Course(selectedCourse.CourseName, selectedCourse.Credits);
                student.Courses.Add(courseCopy);
                _dataService.SaveStudentsToJson();
                Console.WriteLine($"Course '{selectedCourse.CourseName}' added to student successfully.");
            }
            else
            {
                Console.WriteLine("Invalid course selection.");
            }
        }

        private void AddGrade(Student student)
        {
            if (student.Courses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("Select course:");
            for (var i = 0; i < student.Courses.Count; i++)
                Console.WriteLine($"{i + 1}. {student.Courses[i].CourseName}");

            if (int.TryParse(Console.ReadLine(), out var courseIndex) && courseIndex > 0 && courseIndex <= student.Courses.Count)
            {
                var course = student.Courses[courseIndex - 1];
                Console.Write("Enter grade (1-6): ");
                if (decimal.TryParse(Console.ReadLine(), out var grade) && grade >= 1 && grade <= 6)
                {
                    course.Grades.Add(grade);
                    _dataService.SaveStudentsToJson();
                    Console.WriteLine($"Grade {grade} added to {course.CourseName}");
                }
            }
        }
    }
} 