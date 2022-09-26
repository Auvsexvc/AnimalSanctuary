using WebApp.Data;
using WebApp.Dtos;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> GetAccount(LoginDto dto);

        Task<IEnumerable<Account>> GetAllAccounts(string accessToken);

        Task<HttpResponseMessage?> RegisterAsync(RegisterViewModel dto, string accessToken);

        Task<NewUserDropdownsVM> GetNewUserDropdownsVM(string accessToken);
    }
}