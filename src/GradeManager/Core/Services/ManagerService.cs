using Domain.Entities;

namespace Core.Services
{
    public class ManagerService
    {
        private readonly DataService _dataService;
        public List<Student> Students => _dataService.Students;
        public List<Course> AvailableCourses => _dataService.AvailableCourses;

        public ManagerService()
        {
            _dataService = new DataService();
            _dataService.LoadFromJson();
        }

        public void ManageCourses()
        {
            while (true)
            {
                Console.WriteLine("\nCourse Management");
                Console.WriteLine("1. Add New Course");
                Console.WriteLine("2. List Available Courses");
                Console.WriteLine("3. Return to Main Menu");
                
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddNewCourse();
                        break;
                    case "2":
                        ListAvailableCourses();
                        break;
                    case "3":
                        return;
                }
            }
        }

        private void AddNewCourse()
        {
            Console.Write("Enter course name: ");
            var name = Console.ReadLine();
            Console.Write("Enter credits: ");
            if (int.TryParse(Console.ReadLine(), out var credits))
            {
                var course = new Course(name, credits);
                _dataService.AddCourse(course);
                Console.WriteLine("Course added successfully.");
            }
        }

        private void ListAvailableCourses()
        {
            if (_dataService.AvailableCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.WriteLine("\nAvailable Courses:");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine("#".PadRight(4) + "Course Name".PadRight(30) + "Credits");
            Console.WriteLine("------------------------------------------");
            
            for (int i = 0; i < _dataService.AvailableCourses.Count; i++)
            {
                var course = _dataService.AvailableCourses[i];
                Console.WriteLine($"{(i + 1).ToString().PadRight(4)}{course.CourseName.PadRight(30)}{course.Credits}");
            }
        }

        public void CreateStudent()
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

            ListAvailableCourses();
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
