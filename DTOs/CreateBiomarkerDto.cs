﻿namespace ElectronicHealthRecord.DTOs
{
    public class CreateBiomarkerDto
    {
        public string Type { get; set; } = null!;
        public double Value { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; } = null!;
    }
}
