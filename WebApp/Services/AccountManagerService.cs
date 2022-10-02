using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Extensions;
using WebApp.Helpers;
using WebApp.Interfaces;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public sealed class AccountManagerService : IAccountManagerService
    {
        private readonly ILogger<AccountManagerService> _logger;

        public List<AccountModel> Accounts { get; set; } = new List<AccountModel>();

        public AccountManagerService(ILogger<AccountManagerService> logger)
        {
            _logger = logger;
        }

        public AccountModel SignIn(string sessionId, Account account)
        {
            try
            {
                var accountModel = new AccountModel()
                {
                    Id = account.Id,
                    Email = account.Email,
                    Role = account.Role,
                    Token = account.Token,
                    ValidTo = account.ValidTo,
                    SessionId = sessionId,
                };

                Accounts.Add(accountModel);

                return accountModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                throw;
            }
        }

        public ClaimsPrincipal GetIdentity(AccountModel accountModel)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, accountModel.Id),
                new Claim(ClaimTypes.Name, accountModel.Email),
                new Claim(ClaimTypes.Role, accountModel.Role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public AccountModel? GetAccount(string? sessionId)
        {
            if (sessionId == null)
            {
                return default;
            }

            return Accounts.Find(i => i.SessionId == sessionId);
        }

        public bool SignOut(string sessionId)
        {
            var accountModel = GetAccount(sessionId);

            if (accountModel != null)
            {
                return Accounts.Remove(accountModel);
            }

            return false;
        }

        public string? GetTokenBySessionId(string? sessionId)
        {
            var accountModel = Accounts.Find(x => x.SessionId == sessionId);

            if (accountModel == null)
            {
                return null;
            }

            return accountModel.Token;
        }

        public IEnumerable<AccountViewModel> GetAccounts(IEnumerable<Account> accounts)
        {
            return accounts
                .Where(x => !Accounts.Select(x => x.Email).Contains(x.Email))
                .Select(x => new AccountModel() { Email = x.Email, Id = x.Id, Role = x.Role })
                .Concat(Accounts)
                .Select(x => x.ToViewModel())
                .OrderByDescending(x => x.ValidTo);
        }
    }
}