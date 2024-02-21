namespace Logicea_Cards.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Role { get; set; } // "Member" or "Admin"
        public string PasswordHash { get; set; }
    }
}
