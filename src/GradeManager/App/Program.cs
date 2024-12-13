using Core.Services;
using Domain.Entities;

public class Program
{
    private static ManagerService Service = null!;
    public static void Main(string[] args)
    {
        Service = new ManagerService();
        var program = new Program();
        program.Run();
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("\nStudent Management System");
            Console.WriteLine("1. Create Student");
            Console.WriteLine("2. Select Student");
            Console.WriteLine("3. Show Student Courses and Grades");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Service.CreateStudent();
                    break;
                case "2":
                    SelectStudent();
                    break;
                case "3":
                    ShowStudentCoursesAndGrades();
                    break;
                case "4":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void SelectStudent()
    {
        if (Service.Students.Count == 0)
        {
            Console.WriteLine("No students available. Please create a student first.");
            return;
        }

        Console.WriteLine("Available students:");
        for (var i = 0; i < Service.Students.Count; i++)
            Console.WriteLine($"{i + 1}. {Service.Students[i].Name} (ID: {Service.Students[i].StudentId})");

        Console.Write("Enter the number of the student to select: ");
        if (int.TryParse(Console.ReadLine(), out var selectedIndex) && selectedIndex > 0 &&
            selectedIndex <= Service.Students.Count)
        {
            var selectedStudent = Service.Students[selectedIndex - 1];
            Service.ManageStudentCourses(selectedStudent);
        }
        else
        {
            Console.WriteLine("Invalid selection. Please try again.");
        }
    }

    private void ShowStudentCoursesAndGrades()
    {
        if (Service.Students.Count == 0)
        {
            Console.WriteLine("No students available. Please create a student first.");
            return;
        }

        Console.WriteLine("\n=== Student Grades Report ===");
        bool isFirst = true;
        
        foreach (var student in Service.Students)
        {
            if (!isFirst)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("\n" + new string('=', 66));
                Console.ResetColor();
            }
            isFirst = false;

            Console.WriteLine($"\nStudent: {student.Name}");
            Console.WriteLine($"ID: {student.StudentId}");
            
            if (student.Courses.Count == 0)
            {
                Console.WriteLine("No courses registered.");
                continue;
            }

            Console.WriteLine("\nCourses and Grades:");
            Console.WriteLine("------------------------------------------------------------------");
            Console.WriteLine("Course Name".PadRight(30) + "Credits".PadRight(10) + "Grade");
            Console.WriteLine("------------------------------------------------------------------");

            double totalGradePoints = 0;
            int totalCredits = 0;

            foreach (var course in student.Courses)
            {
                Console.WriteLine($"{course.CourseName.PadRight(30)}{course.Credits.ToString().PadRight(10)}{course.Grade:F1}");
                totalGradePoints += course.Grade * course.Credits;
                totalCredits += course.Credits;
            }

            Console.WriteLine("------------------------------------------------------------------");
            if (totalCredits > 0)
            {
                double gpa = totalGradePoints / (totalCredits * 100) * 4.0;
                Console.WriteLine($"GPA: {gpa:F2}");
            }
            Console.WriteLine($"Total Credits: {totalCredits}");
        }
    }
}