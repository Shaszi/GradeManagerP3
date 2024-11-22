using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Core.Services
{
    public class Student_Enrollment
    {
        public Student_Enrollment(Student student, Course course)
        {
            Student = student;
            Course = course;
            EnrollmentDate = DateTime.Now;
        }

        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public DateTime EnrollmentDate { get; private set; }

        public void UpdateEnrollment(Course newCourse)
        {
            Course = newCourse;
        }

        public void CancelEnrollment()
        {
            // Logic for cancelling enrollment
        }
    }
}
