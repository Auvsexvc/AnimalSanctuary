using AnimalSanctuaryAPI.Interfaces;

namespace AnimalSanctuaryAPI.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly AppDbContext _appDbContext;

        public DbInitializer(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task EnsureDbCreatedIfPossibleAsync()
        {
            try
            {
                await _appDbContext!.Database.CanConnectAsync();
                await _appDbContext.Database.EnsureCreatedAsync();
            }
            catch
            {
                await _appDbContext.DisposeAsync();
            }
        }
    }
}