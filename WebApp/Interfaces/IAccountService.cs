using WebClientApp.Data;
using WebClientApp.Dtos;
using WebClientApp.ViewModels;

namespace WebClientApp.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> GetByLoginAsync(LoginDto dto);

        Task<IEnumerable<Account>> GetAllAsync(string accessToken);

        Task<HttpResponseMessage?> RegisterAsync(RegisterViewModel dto, string accessToken);

        Task<NewUserDropdownsVM> GetNewUserDropdownsVMAsync(string accessToken);
    }
}