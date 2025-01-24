using Domain.Entities;

namespace Core.Services
{
    public class CourseManagementService
    {
        private readonly DataService _dataService;

        public CourseManagementService(DataService dataService)
        {
            _dataService = dataService;
        }

        public void ManageCourses()
        {
            while (true)
            {
                Console.WriteLine("\nCourse Management");
                Console.WriteLine("1. Add New Course");
                Console.WriteLine("2. List Available Courses");
                Console.WriteLine("3. Delete Course");
                Console.WriteLine("4. Return to Main Menu");
                
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
                        DeleteCourse();
                        break;
                    case "4":
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

        public void ListAvailableCourses()
        {
            if (_dataService.AvailableCourses.Count == 0)
            {
                Console.WriteLine("No courses available.");
                return;
            }

            Console.Clear();
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

        public void DeleteCourse()
        {
            if (_dataService.AvailableCourses.Count == 0)
            {
                Console.WriteLine("No courses available to delete.");
                return;
            }

            ListAvailableCourses();
            Console.Write("\nEnter the course number to delete: ");
            if (int.TryParse(Console.ReadLine(), out var courseIndex) && 
                courseIndex > 0 && courseIndex <= _dataService.AvailableCourses.Count)
            {
                var courseToDelete = _dataService.AvailableCourses[courseIndex - 1];
                _dataService.AvailableCourses.Remove(courseToDelete);
                _dataService.SaveCoursesToJson();
                Console.WriteLine($"Course '{courseToDelete.CourseName}' deleted successfully.");
            }
            else
            {
                Console.WriteLine("Invalid course selection.");
            }
        }
    }
} 