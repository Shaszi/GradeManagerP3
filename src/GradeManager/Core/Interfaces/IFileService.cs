namespace Core.Services;

using Domain.Entities;

public interface IFileService
{
    void SaveToFile(string content, string path);
    string ReadFromFile(string path);
    List<Student> GetStudents();
    void SaveStudents(List<Student> students);
    List<Course> GetCourses();
    void SaveCourses(List<Course> courses);
}