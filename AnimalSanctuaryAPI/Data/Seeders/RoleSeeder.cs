using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.Interfaces;

namespace AnimalSanctuaryAPI.Data.Seeders
{
    public sealed class RoleSeeder : IRoleSeeder
    {
        private readonly AppDbContext _dbContext;

        public RoleSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Administrator"
                },
            };
            return roles;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync() && !_dbContext.Roles.Any())
            {
                var roles = GetRoles();
                _dbContext.Roles.AddRange(roles);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}