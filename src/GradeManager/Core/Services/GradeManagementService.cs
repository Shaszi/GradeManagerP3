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
                Console.Clear();
                Console.WriteLine($"\nManaging courses for {student.Name}");
                Console.WriteLine("1. Add Course from Available Courses");
                Console.WriteLine("2. Add Grade");
                Console.WriteLine("3. Remove Course");
                Console.WriteLine("4. Return to Main Menu");
                
                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1":
                        AddCourseToStudent(student);
                        break;
                    case "2":
                        AddGrade(student);
                        break;
                    case "3":
                        RemoveCourseFromStudent(student);
                        break;
                    case "4":
                        return;
                }
                
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
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
                    course.FinalGrade = course.Grades.Sum() / course.Grades.Count;
                    _dataService.SaveStudentsToJson();
                    Console.WriteLine($"Grade {grade} added to {course.CourseName}");
                    Console.WriteLine($"Final grade is now: {course.FinalGrade:F2}");
                }
                else
                {
                    Console.WriteLine("Invalid grade. Please enter a number between 1 and 6.");
                }
            }
            else
            {
                Console.WriteLine("Invalid course selection.");
            }
        }

        private void RemoveCourseFromStudent(Student student)
        {
            if (student.Courses.Count == 0)
            {
                Console.WriteLine("Student has no courses to remove.");
                return;
            }

            Console.WriteLine("\nAvailable courses:");
            for (var i = 0; i < student.Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {student.Courses[i].CourseName}");
            }

            Console.Write("\nEnter the number of the course to remove: ");
            if (int.TryParse(Console.ReadLine(), out var courseIndex) && 
                courseIndex > 0 && 
                courseIndex <= student.Courses.Count)
            {
                var courseToRemove = student.Courses[courseIndex - 1];
                student.Courses.RemoveAt(courseIndex - 1);
                _dataService.SaveStudentsToJson();
                Console.WriteLine($"Course '{courseToRemove.CourseName}' removed successfully.");
            }
            else
            {
                Console.WriteLine("Invalid course selection.");
            }
        }
    }
} 