﻿

namespace Domain.Entities
{
    public class Course
    {
        public Course(string courseName, int credits)
        {
            CourseName = courseName;
            Credits = credits;
        }

        public string CourseName { get; private set; }
        public int Credits { get; private set; } // New field for course credits
        public double Grade { get; set; }
    }
}