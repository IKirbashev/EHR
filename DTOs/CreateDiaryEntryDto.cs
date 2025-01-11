namespace ElectronicHealthRecord.DTOs
{
    public class CreateDiaryEntryDto
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Attachments { get; set; } = null!;
        public int FolderId { get; set; }
        public string UserId { get; set; } = null!;
    }
}
