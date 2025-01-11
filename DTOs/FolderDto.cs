namespace ElectronicHealthRecord.DTOs
{
    public class FolderDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public IEnumerable<int> DiaryEntryIds { get; set; }
    }
}
