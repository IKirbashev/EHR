namespace ElectronicHealthRecord.Models
{
    public class CalendarEvent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string EventType { get; set; }

        // Связь
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
