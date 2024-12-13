using Domain.Entities;

namespace Core.Services
{
    public class ManagerService
    {
        private readonly DataService _dataService;
        public List<Student> Students => _dataService.Students;

        public ManagerService()
        {
            _dataService = new DataService();
            _dataService.LoadFromJson();
        }

        public void CreateStudent()
        {
            Console.Write("Enter student name: ");
            var name = Console.ReadLine();
            Console.Write("Enter student ID: ");
            var studentId = Console.ReadLine();

            var newStudent = new Student(name, studentId);
            _dataService.Students.Add(newStudent);
            _dataService.SaveToJson();

            Console.WriteLine($"Student {name} with ID {studentId} created successfully.");
        }

        public void ManageStudentCourses(Student student)
        {
            while (true)
            {
                Console.WriteLine($"\nManaging courses for {student.Name}");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Add Grade");
                Console.WriteLine("3. Return to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCourse(student);
                        break;
                    case "2":
                        AddGrade(student);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void AddCourse(Student student)
        {
            Console.Write("Enter course name: ");
            var courseName = Console.ReadLine();
            Console.Write("Enter course credits: ");
            if (int.TryParse(Console.ReadLine(), out var credits))
            {
                var newCourse = new Course(courseName, credits);
                student.Courses.Add(newCourse);
                _dataService.SaveToJson();
                Console.WriteLine($"Course {courseName} added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid credits. Course not added.");
            }
        }

        private void AddGrade(Student student)
        {
            if (student.Courses.Count == 0)
            {
                Console.WriteLine("No courses available. Please add a course first.");
                return;
            }

            Console.WriteLine("Available courses:");
            for (var i = 0; i < student.Courses.Count; i++)
                Console.WriteLine($"{i + 1}. {student.Courses[i].CourseName}");

            Console.Write("Enter the number of the course to add a grade: ");
            if (int.TryParse(Console.ReadLine(), out var selectedIndex) && selectedIndex > 0 &&
                selectedIndex <= student.Courses.Count)
            {
                var selectedCourse = student.Courses[selectedIndex - 1];
                Console.Write("Enter the grade: ");
                if (double.TryParse(Console.ReadLine(), out var grade))
                {
                    selectedCourse.Grade = grade;
                    _dataService.SaveToJson();
                    Console.WriteLine($"Grade {grade} added successfully for {selectedCourse.CourseName}.");
                }
                else
                {
                    Console.WriteLine("Invalid grade. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid selection. Please try again.");
            }
        }
    }
}
