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
            Console.WriteLine("1. Manage Students");
            Console.WriteLine("2. Manage Courses");
            Console.WriteLine("3. Select Student");
            Console.WriteLine("4. Show Student Courses and Grades");
            Console.WriteLine("5. Exit");
            Console.Write("Enter your choice: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Service.ManageStudents();
                    break;
                case "2":
                    Service.ManageCourses();
                    break;
                case "3":
                    SelectStudent();
                    break;
                case "4":
                    ShowStudentCoursesAndGrades();
                    break;
                case "5":
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
            while (true)
            {
                Console.WriteLine($"\nSelected Student: {selectedStudent.Name} (ID: {selectedStudent.StudentId})");
                Console.WriteLine("1. Manage Courses and Grades");
                Console.WriteLine("2. Show Student Information");
                Console.WriteLine("3. Return to Main Menu");
                Console.Write("Enter your choice: ");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Service.ManageStudentCourses(selectedStudent);
                        break;
                    case "2":
                        ShowStudentInformation(selectedStudent);
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid selection. Please try again.");
        }
    }

    private void ShowStudentInformation(Student student)
    {
        Console.WriteLine($"\nStudent Information for {student.Name} (ID: {student.StudentId})");
        Console.WriteLine("------------------------------------------------------------------");
        
        if (!student.Courses.Any())
        {
            Console.WriteLine("No courses enrolled.");
            return;
        }

        foreach (var course in student.Courses)
        {
            Console.WriteLine($"\nCourse: {course.CourseName} (Credits: {course.Credits})");
            if (course.Grades.Any())
            {
                Console.WriteLine("Grades: " + string.Join(", ", course.Grades));
                Console.WriteLine($"Final Grade: {course.FinalGrade:F2}");
            }
            else
            {
                Console.WriteLine("No grades recorded");
            }
        }
        
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    private void ShowStudentCoursesAndGrades()
    {
        if (!Service.Students.Any())
        {
            Console.WriteLine("\nNo students registered in the system.");
            return;
        }

        foreach (var student in Service.Students)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n══════════════════════════════════════════════════════════════════");
            Console.ResetColor();
            
            Console.WriteLine($"\nStudent: {student.Name} (ID: {student.StudentId})");
            
            if (!student.Courses.Any())
            {
                Console.WriteLine("No courses enrolled for this student.");
                continue;
            }

            Console.WriteLine("Courses and Grades:");
            
            foreach (var course in student.Courses)
            {
                Console.WriteLine($"\nCourse: {course.CourseName} (Credits: {course.Credits})");
                if (course.Grades.Any())
                {
                    Console.WriteLine("Grades: " + string.Join(", ", course.Grades));
                    Console.WriteLine($"Final Grade: {course.FinalGrade:F2}");
                }
                else
                {
                    Console.WriteLine("No grades recorded for this course");
                }
            }
        }
        
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n══════════════════════════════════════════════════════════════════");
        Console.ResetColor();
    }
}