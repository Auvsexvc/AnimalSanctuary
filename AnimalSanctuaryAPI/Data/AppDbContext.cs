using AnimalSanctuaryAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnimalSanctuaryAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Animal> Animals => Set<Animal>();
        public DbSet<AnimalType> Types => Set<AnimalType>();
        public DbSet<AnimalSpecie> Species => Set<AnimalSpecie>();
        public DbSet<Facility> Facilities => Set<Facility>();

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}