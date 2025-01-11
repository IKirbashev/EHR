namespace ElectronicHealthRecord.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public string Schedule { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Relationship
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
