namespace ElectronicHealthRecord.DTOs
{
    public class UpdateCalendarEventDto
    {
        public DateTime Date { get; set; }
        public string Description { get; set; } = null!;
        public string EventType { get; set; } = null!;
    }
}
