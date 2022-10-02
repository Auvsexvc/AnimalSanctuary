using WebApp.Data;
using WebApp.Dtos;
using WebApp.ViewModels;

namespace WebApp.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> GetByLoginAsync(LoginDto dto);

        Task<IEnumerable<Account>> GetAllAsync(string accessToken);

        Task<HttpResponseMessage?> RegisterAsync(RegisterViewModel dto, string accessToken);

        Task<NewUserDropdownsVM> GetNewUserDropdownsVMAsync(string accessToken);
    }
}