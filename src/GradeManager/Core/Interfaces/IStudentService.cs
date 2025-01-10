namespace Core.Interfaces;

using Domain.Entities;

public interface IStudentService
{
    void AddStudent(Student student);
    void RemoveStudent(string studentId);
    Student GetStudent(string studentId);
    IEnumerable<Student> GetAllStudents();
    void UpdateStudent(Student student);
    void ManageStudentCourses(Student student);
} 