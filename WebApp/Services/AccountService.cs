using System.IdentityModel.Tokens.Jwt;
using WebClientApp.Data;
using WebClientApp.Dtos;
using WebClientApp.Helpers;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels;

namespace WebClientApp.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccountService> _logger;

        public AccountService(ILogger<AccountService> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        public async Task<Account?> GetByLoginAsync(LoginDto dto)
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
                    Id = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value,
                    Email = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value,
                    Role = jwtSecurityToken.Claims.First(claim => claim.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value,
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

        public async Task<IEnumerable<Account>> GetAllAsync(string accessToken)
        {
            try
            {
                var data = Enumerable.Empty<Account>();

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                using var result = await _httpClient.GetAsync("account");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(json))
                    {
                        return (await result.Content.ReadFromJsonAsync<IList<Account>>())!;
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return Enumerable.Empty<Account>();
            }
        }

        public async Task<IEnumerable<Role>> GetAllRoles(string accessToken)
        {
            try
            {
                var data = Enumerable.Empty<Role>();

                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.GetAsync("Account/Roles");

                if (result.IsSuccessStatusCode)
                {
                    var json = await result.Content.ReadAsStringAsync();

                    if (!string.IsNullOrEmpty(json))
                    {
                        return (await result.Content.ReadFromJsonAsync<IList<Role>>())!;
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return Enumerable.Empty<Role>();
            }
        }

        public async Task<HttpResponseMessage?> RegisterAsync(RegisterViewModel dto, string accessToken)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);
                var result = await _httpClient.PostAsJsonAsync("account/register", dto);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                return null;
            }
        }

        public async Task<NewUserDropdownsVM> GetNewUserDropdownsVMAsync(string accessToken) => new NewUserDropdownsVM()
        {
            Roles = (await GetAllRoles(accessToken)).OrderBy(a => a.Name).ToList(),
        };

        private async Task<HttpResponseMessage?> GetToken(LoginDto dto)
        {
            try
            {
                var result = await _httpClient.PostAsJsonAsync("account/login", dto);

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