﻿namespace ElectronicHealthRecord.DTOs
{
    public class UserDto
    {
        public string Id { get; set; } = null!; // Changed from int to string
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = null!;
    }
}
