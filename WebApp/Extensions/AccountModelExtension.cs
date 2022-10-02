using WebClientApp.Models;
using WebClientApp.ViewModels;

namespace WebClientApp.Extensions
{
    public static class AccountModelExtension
    {
        public static AccountViewModel ToViewModel(this AccountModel user)
        {
            return new AccountViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                Role = user.Role,
                Token = user.Token,
                ValidTo = user.ValidTo,
                SessionId = user.SessionId
            };
        }
    }
}