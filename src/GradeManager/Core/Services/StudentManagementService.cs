using Domain.Entities;

namespace Core.Services
{
    public class StudentManagementService
    {
        private readonly DataService _dataService;

        public StudentManagementService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void ManageStudents()
        {
            while (true)
            {
                Console.WriteLine("\nStudent Management");
                Console.WriteLine("1. Create New Student");
                Console.WriteLine("2. List Students");
                Console.WriteLine("3. Delete Student");
                Console.WriteLine("4. Return to Main Menu");
                
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        CreateStudent();
                        break;
                    case "2":
                        ListStudents();
                        break;
                    case "3":
                        DeleteStudent();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateStudent()
        {
            Console.Write("Enter student name: ");
            var name = Console.ReadLine();
            
            string studentId;
            bool isIdValid = false;
            
            do
            {
                Console.Write("Enter student ID: ");
                studentId = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(studentId))
                {
                    Console.WriteLine("Student ID cannot be empty. Please try again.");
                    continue;
                }
                
                if (_dataService.Students.Any(s => s.StudentId.Equals(studentId, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("This Student ID already exists. Please enter a different ID.");
                    continue;
                }
                
                isIdValid = true;
                
            } while (!isIdValid);

            var newStudent = new Student(name, studentId);
            _dataService.Students.Add(newStudent);
            _dataService.SaveStudentsToJson();

            Console.WriteLine($"Student {name} with ID {studentId} created successfully.");
        }

        private void ListStudents()
        {
            if (_dataService.Students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }

            Console.WriteLine("\nRegistered Students:");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("#".PadRight(4) + "Name".PadRight(30) + "ID");
            Console.WriteLine("------------------------------------------");
            
            for (int i = 0; i < _dataService.Students.Count; i++)
            {
                var student = _dataService.Students[i];
                Console.WriteLine($"{(i + 1).ToString().PadRight(4)}{student.Name.PadRight(30)}{student.StudentId}");
            }
        }

        private void DeleteStudent()
        {
            if (_dataService.Students.Count == 0)
            {
                Console.WriteLine("No students available to delete.");
                return;
            }

            ListStudents();
            Console.Write("\nEnter the number of the student to delete: ");
            if (int.TryParse(Console.ReadLine(), out var studentIndex) && 
                studentIndex > 0 && studentIndex <= _dataService.Students.Count)
            {
                var studentToDelete = _dataService.Students[studentIndex - 1];
                _dataService.Students.Remove(studentToDelete);
                _dataService.SaveStudentsToJson();
                Console.WriteLine($"Student '{studentToDelete.Name}' deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid student selection.");
            }
        }
    }
} 