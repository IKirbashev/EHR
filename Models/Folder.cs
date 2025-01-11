namespace ElectronicHealthRecord.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        // Relationships
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public ICollection<DiaryEntry> DiaryEntries { get; set; } = new List<DiaryEntry>();
    }
}
