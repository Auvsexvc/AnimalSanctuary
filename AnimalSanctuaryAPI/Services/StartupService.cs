using AnimalSanctuaryAPI.Interfaces;

namespace AnimalSanctuaryAPI.Services
{
    public sealed class StartupService : IHostedService
    {
        private readonly IServiceProvider _serviceScopeFactory;

        public StartupService(IServiceProvider serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var _appDbInitializer = scope.ServiceProvider.GetService<IDbInitializer>();
            var _appDbRoleSeeder = scope.ServiceProvider.GetService<IRoleSeeder>();

            if (_appDbInitializer != null)
            {
                await _appDbInitializer.EnsureDbCreatedIfPossibleAsync();
            }

            if (_appDbRoleSeeder != null)
            {
                await _appDbRoleSeeder.Seed();
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotSupportedException();
        }
    }
}