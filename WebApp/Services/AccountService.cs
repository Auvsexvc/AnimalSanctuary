using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;

namespace WebApp.Services
{
    public class AccountService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IConfiguration configuration, ILogger<AccountService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<HttpResponseMessage?> GetToken(LoginDto dto)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));

                var result = await client.PostAsJsonAsync<LoginDto>("account/login", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }
    }
}
