namespace ElectronicHealthRecord.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Dosage { get; set; }
        public string Schedule { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Связь
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
