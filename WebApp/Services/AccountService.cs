using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Text;
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

                var token = await result.Content.ReadAsStringAsync();
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                Account account = new()
                {
                    Id = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                    Email = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value,
                    Role = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value,
                    Token = token,
                    ValidTo = jwtSecurityToken.ValidTo
                };

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