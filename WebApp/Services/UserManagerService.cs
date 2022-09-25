using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Services
{
    public class UserManagerService
    {
        private readonly ILogger<UserManagerService> _logger;

        public List<User> Users { get; set; } = new List<User>();

        public UserManagerService(ILogger<UserManagerService> logger)
        {
            _logger = logger;
        }

        public User AddUser(string sessionId, Account account)
        {
            try
            {
                var user = new User()
                {
                    Id = account.Id,
                    Email = account.Email,
                    Role = account.Role,
                    Token = account.Token,
                    ValidTo = account.ValidTo,
                    SessionId = sessionId,
                };

                Users.Add(user);

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(Message.ERROR, ex.Message);

                throw;
            }
        }

        public ClaimsPrincipal GetUserIdentity(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(claimsIdentity);
        }

        public User? GetUser(string? sessionId)
        {
            if (sessionId == null)
            {
                return default;
            }

            return Users.Find(i => i.SessionId == sessionId);
        }

        public bool DeleteUser(string sessionId)
        {
            var user = GetUser(sessionId);

            if (user != null)
            {
                return Users.Remove(user);
            }

            return false;
        }

        public string? GetUserToken(string? sessionId)
        {
            var user = Users.Find(x => x.SessionId == sessionId);

            if (user == null)
            {
                return null;
            }

            return user.Token;
        }
    }
}