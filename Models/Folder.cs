namespace ElectronicHealthRecord.Models
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Связи
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<DiaryEntry> DiaryEntries { get; set; }
    }
}
