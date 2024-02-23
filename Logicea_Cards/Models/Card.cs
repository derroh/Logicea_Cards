namespace Logicea_Cards.Models
{
    public class Card
    {
        public required int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; } // Ensure format validation
        public string Status { get; set; } = "To Do"; // Default status
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public string UserEmail { get; set; } // Foreign key
        public User User { get; set; }
    }
}
