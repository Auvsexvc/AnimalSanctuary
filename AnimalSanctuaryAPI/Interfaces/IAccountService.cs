using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAccountService
    {
        Task<string> GenereateJWT(LoginDto dto);

        Task<IEnumerable<UserDto>> GetAll();

        Task<IEnumerable<Role>> GetRoles();

        Task RegisterUser(RegisterUserDto dto);
    }
}