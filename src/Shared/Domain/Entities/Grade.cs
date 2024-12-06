

namespace Domain.Entities
{
    public class Grade
    {
        public decimal Value { get; set; }
        public required string Title { get; set; }
        public DateTime DateAssigned { get; set; }
    }
}
