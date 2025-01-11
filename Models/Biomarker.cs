namespace ElectronicHealthRecord.Models
{
    public class Biomarker
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public double Value { get; set; }
        public DateTime Date { get; set; }

        // Relationship
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
