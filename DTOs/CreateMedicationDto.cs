﻿namespace ElectronicHealthRecord.DTOs
{
    public class CreateMedicationDto
    {
        public string Name { get; set; } = null!;
        public string Dosage { get; set; } = null!;
        public string Schedule { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UserId { get; set; } = null!;
    }
}
