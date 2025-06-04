namespace VeritasEd.Api.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}