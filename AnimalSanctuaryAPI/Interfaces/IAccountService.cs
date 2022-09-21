using AnimalSanctuaryAPI.Dtos;

namespace AnimalSanctuaryAPI.Interfaces
{
    public interface IAccountService
    {
        string GenereateJWT(LoginDto dto);
        IEnumerable<UserDto> GetAll();
        void RegisterUser(RegisterUserDto dto);
    }
}