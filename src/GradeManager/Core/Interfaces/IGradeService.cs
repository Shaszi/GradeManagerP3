public interface IGradeService
{
    bool AddGradeToStudent(string studentId, string courseName, int grade);
    bool RemoveLastGrade(string studentId, string courseName);
} 