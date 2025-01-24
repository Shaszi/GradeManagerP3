using Domain.Entities;

namespace Core.Services
{
    public class StudentService
    {
        public void ManageStudentCourses(Student student)
        {
            while (true)
            {
                Console.WriteLine($"\nManaging courses for student: {student.Name}");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Add Grade to Course");
                Console.WriteLine("3. Return to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCourse(student);
                        break;
                    case "2":
                        AddGradeToCourse(student);
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

            if (string.IsNullOrWhiteSpace(courseName))
            {
                Console.WriteLine("Course name cannot be empty.");
                return;
            }

            if (student.Courses.Any(c => c.CourseName.Equals(courseName, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine($"Student already has course '{courseName}'. Cannot add duplicate courses.");
                return;
            }

            Console.Write("Enter course credits: ");
            if (!int.TryParse(Console.ReadLine(), out var credits))
            {
                Console.WriteLine("Invalid credits. Please enter a number.");
                return;
            }

            var course = new Course(courseName, credits);
            student.Courses.Add(course);
            Console.WriteLine($"Course '{courseName}' added successfully.");
        }

        private void AddGradeToCourse(Student student)
        {
            if (student.Courses.Count == 0)
            {
                Console.WriteLine("No courses available. Please add a course first.");
                return;
            }

            Console.WriteLine("Available courses:");
            for (var i = 0; i < student.Courses.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {student.Courses[i].CourseName}");
            }

            Console.Write("Select course number: ");
            if (!int.TryParse(Console.ReadLine(), out var courseIndex) ||
                courseIndex < 1 || courseIndex > student.Courses.Count)
            {
                Console.WriteLine("Invalid course selection.");
                return;
            }

            Console.Write("Enter grade (0-100): ");
            if (!decimal.TryParse(Console.ReadLine(), out var grade) || grade < 0 || grade > 100)
            {
                Console.WriteLine("Invalid grade. Grade must be between 0 and 100.");
                return;
            }

            var selectedCourse = student.Courses[courseIndex - 1];
            selectedCourse.Grades.Add(grade);
            Console.WriteLine($"Grade {grade} added to course '{selectedCourse.CourseName}' successfully.");
        }
    }
}