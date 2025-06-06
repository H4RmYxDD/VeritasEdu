﻿namespace VeritasEd.Api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // For demo only; use hashing in production!
        public string Role { get; set; } = string.Empty; // "Student" or "Teacher"
    }
}