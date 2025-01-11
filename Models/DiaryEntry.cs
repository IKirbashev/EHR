namespace ElectronicHealthRecord.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Attachments { get; set; } = null!; // List of file paths or URLs

        // Relationships
        public int FolderId { get; set; }
        public Folder Folder { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
    }
}
