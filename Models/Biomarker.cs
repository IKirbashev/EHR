namespace ElectronicHealthRecord.Models
{
    public class Biomarker
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public DateTime Date { get; set; }

        // Связь
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
