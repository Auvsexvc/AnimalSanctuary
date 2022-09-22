using WebApp.Data;
using WebApp.Dtos;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class AccountService : IAccountService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountService> _logger;

        public AccountService(IConfiguration configuration, ILogger<AccountService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Account?> GetAccount(LoginDto dto)
        {
            try
            {
                using var result = await GetToken(dto);

                if (result?.IsSuccessStatusCode != true)
                {
                    return null;
                }

                var account = await result.Content.ReadFromJsonAsync<Account>();

                return account;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<IEnumerable<Account>> GetAllAccounts(string accessToken)
        {
            try
            {
                var data = Enumerable.Empty<Account>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                    var result = await client.GetAsync("account");

                    if (result.IsSuccessStatusCode)
                    {
                        var json = await result.Content.ReadAsStringAsync();

                        if (!string.IsNullOrEmpty(json))
                        {
                            return (await result.Content.ReadFromJsonAsync<IList<Account>>())!;
                        }
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                throw;
            }
        }

        public async Task<HttpResponseMessage> RegisterAsync(RegisterVM dto, string accessToken)
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(_configuration.GetConnectionString("DefaultConnection"));
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await client.PostAsJsonAsync("account/register", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                throw;
            }
        }

        private async Task<HttpResponseMessage?> GetToken(LoginDto dto)
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