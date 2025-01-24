using Domain.Entities;

namespace Core.Services;

public class GradeService : IGradeService
{
    private readonly IFileService _fileService;

    public GradeService(IFileService fileService)
    {
        _fileService = fileService;
    }

    public bool AddGradeToStudent(string studentId, string courseName, int grade)
    {
        try
        {
            var students = _fileService.GetStudents();
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                return false;
            }

            var course = student.Courses.FirstOrDefault(c => c.CourseName == courseName);
            if (course == null)
            {
                return false;
            }

            course.Grades.Add(grade);
            course.FinalGrade = course.Grades.Average();
            _fileService.SaveStudents(students);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool RemoveLastGrade(string studentId, string courseName)
    {
        try
        {
            var students = _fileService.GetStudents();
            var student = students.FirstOrDefault(s => s.StudentId == studentId);
            if (student == null)
            {
                return false;
            }

            var course = student.Courses.FirstOrDefault(c => c.CourseName == courseName);
            if (course == null || !course.Grades.Any())
            {
                return false;
            }

            course.Grades.RemoveAt(course.Grades.Count - 1);
            course.FinalGrade = course.Grades.Any() ? course.Grades.Average() : 0;
            _fileService.SaveStudents(students);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
