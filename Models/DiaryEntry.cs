namespace ElectronicHealthRecord.Models
{
    public class DiaryEntry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public string Attachments { get; set; } // Список путей к файлам или ссылки

        // Связи
        public int FolderId { get; set; }
        public Folder Folder { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
