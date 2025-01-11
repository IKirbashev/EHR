namespace ElectronicHealthRecord.DTOs
{
    public class CalendarEventDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public string EventType { get; set; } = null!;
        public int UserId { get; set; }
    }
}
