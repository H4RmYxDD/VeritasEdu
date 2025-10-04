namespace VeritasEd.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string ProfileImageUrl { get; set; } = "default_profile.png";

        public override bool Equals(object? obj)
        {
            if (obj is not User other)
                return false;

            return this.Id == other.Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Username;
        }
    }
}