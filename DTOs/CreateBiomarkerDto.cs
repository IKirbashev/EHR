namespace ElectronicHealthRecord.DTOs
{
    public class CreateBiomarkerDto
    {
        public string Type { get; set; } = null!;
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
    }
}
