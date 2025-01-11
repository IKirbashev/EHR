namespace ElectronicHealthRecord.DTOs
{
    public class FolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; } = null!;
        public IEnumerable<int> DiaryEntryIds { get; set; }
    }
}
