using Microsoft.AspNetCore.Identity;

namespace ElectronicHealthRecord.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }

        // Relationships
        public ICollection<Biomarker> Biomarkers { get; set; } = new List<Biomarker>();
        public ICollection<Medication> Medications { get; set; } = new List<Medication>();
        public ICollection<CalendarEvent> CalendarEvents { get; set; } = new List<CalendarEvent>();
        public ICollection<Folder> Folders { get; set; } = new List<Folder>();
        public ICollection<DiaryEntry> DiaryEntries { get; set; } = new List<DiaryEntry>();
    }
}
