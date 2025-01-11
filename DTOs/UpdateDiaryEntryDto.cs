namespace ElectronicHealthRecord.DTOs
{
    public class UpdateDiaryEntryDto
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Attachments { get; set; } = null!;
        public int FolderId { get; set; }
    }
}
