using Microsoft.EntityFrameworkCore;

namespace Logicea_Cards.Models
{
    public class CardsDbContext : DbContext
    {
        public CardsDbContext(DbContextOptions<CardsDbContext> options) : base(options)
        {

        }
    }
}
