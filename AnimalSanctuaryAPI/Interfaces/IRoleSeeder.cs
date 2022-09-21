using AnimalSanctuaryAPI.Entities;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IRoleSeeder
    {
        IEnumerable<Role> GetRoles();
        Task Seed();
    }
}