namespace ElectronicHealthRecord.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public string EventType { get; set; } = null!;

        // Relationship
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
