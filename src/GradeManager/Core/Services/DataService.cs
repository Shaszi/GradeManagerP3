using System.Text.Json;
using Domain.Entities;

namespace Core.Services
{
    public class DataService
    {
        private const string STUDENTS_FILE_PATH = "data/students.json";
        private const string COURSES_FILE_PATH = "data/courses.json";
        
        public List<Student> Students { get; private set; } = new List<Student>();
        public List<Course> AvailableCourses { get; private set; } = new List<Course>();

        public DataService()
        {
            Directory.CreateDirectory("data");
            LoadFromJson();
        }

        public void SaveStudentsToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            string jsonString = JsonSerializer.Serialize(Students, options);
            File.WriteAllText(STUDENTS_FILE_PATH, jsonString);
        }

        public void SaveCoursesToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            string jsonString = JsonSerializer.Serialize(AvailableCourses, options);
            File.WriteAllText(COURSES_FILE_PATH, jsonString);
        }

        public void LoadFromJson()
        {
            if (File.Exists(STUDENTS_FILE_PATH))
            {
                string jsonString = File.ReadAllText(STUDENTS_FILE_PATH);
                Students = JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
            }

            if (File.Exists(COURSES_FILE_PATH))
            {
                string jsonString = File.ReadAllText(COURSES_FILE_PATH);
                AvailableCourses = JsonSerializer.Deserialize<List<Course>>(jsonString) ?? new List<Course>();
            }
        }

        public void AddCourse(Course course)
        {
            if (!AvailableCourses.Any(c => c.CourseName.Equals(course.CourseName, StringComparison.OrdinalIgnoreCase)))
            {
                AvailableCourses.Add(course);
                SaveCoursesToJson();
            }
        }

        public void SaveAll()
        {
            SaveStudentsToJson();
            SaveCoursesToJson();
        }
    }
}
