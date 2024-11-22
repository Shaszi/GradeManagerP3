using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;


namespace Core.Services
{
    internal class GradeServices
    {
        private static void AddCourse(Student student)
        {
            Console.Write("Enter course name: ");
            var courseName = Console.ReadLine();
            Console.Write("Enter course credits: ");
            if (int.TryParse(Console.ReadLine(), out var credits))
            {
                var newCourse = new Course(courseName, credits);
                student.Courses.Add(newCourse);
                Console.WriteLine($"Course {courseName} added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid credits. Course not added.");
            }
        }
    }
}
