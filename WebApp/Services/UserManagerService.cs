using WebApp.Data;
using WebApp.Models;

namespace WebApp.Services
{
    public class UserManagerService
    {
        public List<User> Users { get; set; } = new List<User>();

        public UserManagerService()
        {
        }

        public void AddUser(string sessionId, Account account)
        {
            var user = new User()
            {
                Email = account.Email,
                Role = account.Role,
                Token = account.Token,
                SessionId = sessionId,
            };

            Users.Add(user);
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
    }
}