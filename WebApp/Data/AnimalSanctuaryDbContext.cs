using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Data
{
    public class AnimalSanctuaryDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }

        public AnimalSanctuaryDbContext(DbContextOptions<AnimalSanctuaryDbContext> options) : base(options)
        {
        }
    }
}
