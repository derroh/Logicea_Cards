namespace Logicea_Cards.Models
{
    public class User
    {
        public string Email { get; set; }
        public string Role { get; set; } // "Member" or "Admin"
        public string Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
