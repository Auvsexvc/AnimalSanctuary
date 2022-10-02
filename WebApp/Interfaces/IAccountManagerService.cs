using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAccountManagerService
    {
        List<AccountModel> Accounts { get; set; }

        AccountModel? GetAccount(string? sessionId);
        IEnumerable<AccountViewModel> GetAccounts(IEnumerable<Account> accounts);
        ClaimsPrincipal GetIdentity(AccountModel accountModel);
        string? GetTokenBySessionId(string? sessionId);
        AccountModel SignIn(string sessionId, Account account);
        bool SignOut(string sessionId);
    }
}