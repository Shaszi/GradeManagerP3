using System.Text.Json;
using Domain.Entities;

namespace Core.Services
{
    public class DataService
    {
        private const string FILE_PATH = "students.json";
        public List<Student> Students { get; private set; } = new List<Student>();

        public void SaveToJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            
            string jsonString = JsonSerializer.Serialize(Students, options);
            File.WriteAllText(FILE_PATH, jsonString);
        }

        public void LoadFromJson()
        {
            if (File.Exists(FILE_PATH))
            {
                string jsonString = File.ReadAllText(FILE_PATH);
                Students = JsonSerializer.Deserialize<List<Student>>(jsonString) ?? new List<Student>();
            }
        }
    }
}
