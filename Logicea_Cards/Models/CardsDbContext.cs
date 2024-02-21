using Microsoft.EntityFrameworkCore;

namespace Logicea_Cards.Models
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasKey(c => new { c.Id });
            modelBuilder.Entity<User>()
                .HasKey(c => new { c.Email });
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
