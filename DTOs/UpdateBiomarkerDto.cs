namespace ElectronicHealthRecord.DTOs
{
    public class UpdateBiomarkerDto
    {
        public string Type { get; set; } = null!;
        public double Value { get; set; }
        public DateTime Date { get; set; }
    }
}
