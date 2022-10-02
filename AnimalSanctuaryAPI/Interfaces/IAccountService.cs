using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAccountService
    {
        Task<string> GenereateJWTAsync(LoginDto dto);

        Task<IEnumerable<UserDto>> GetAllAccountsAsync();

        Task<IEnumerable<Role>> GetRolesAsync();

        Task RegisterAccountAsync(RegisterUserDto dto);
    }
}