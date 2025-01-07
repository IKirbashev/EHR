namespace ElectronicHealthRecord.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Связи
        public ICollection<Biomarker> Biomarkers { get; set; }
        public ICollection<Medication> Medications { get; set; }
        public ICollection<CalendarEvent> CalendarEvents { get; set; }
        public ICollection<Folder> Folders { get; set; }
    }
}
