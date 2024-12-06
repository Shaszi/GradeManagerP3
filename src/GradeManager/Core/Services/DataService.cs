using Domain.Entities;

namespace Core.Services
{
    public class DataService
    {
        public List<Student> Students { get; private set; } = new List<Student>();
    }
}
