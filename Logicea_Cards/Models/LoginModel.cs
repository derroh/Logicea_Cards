namespace Logicea_Cards.Models
{
    public class LoginModel
    {
        public string? EmailAddress { get; set; }
        public string? Password { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
