using AnimalSanctuaryAPI.Auth;
using AnimalSanctuaryAPI.Data;
using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.Exceptions;
using AnimalSanctuaryAPI.Helpers;
using AnimalSanctuaryAPI.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AnimalSanctuaryAPI.Services
{
    public sealed class AccountService : IAccountService
    {
        private readonly AppDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AccountService> _logger;
        private readonly AuthenticationSettings _authenticationSettings;

        public AccountService(AppDbContext context, IPasswordHasher<User> passwordHasher, ILogger<AccountService> logger, AuthenticationSettings authenticationSettings)
        {
            _dbContext = context;
            _passwordHasher = passwordHasher;
            _logger = logger;
            _authenticationSettings = authenticationSettings;
        }

        public async Task<string> GenereateJWTAsync(LoginDto dto)
        {
            var user = await _dbContext
                .Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiresOn = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer, _authenticationSettings.JwtIssuer, claims, expires: expiresOn, signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(token);
        }

        public async Task<IEnumerable<UserDto>> GetAllAccountsAsync()
        {
            return await _dbContext
                .Users
                .Include(r => r.Role)
                .Select(u => ToDto(u))
                .ToListAsync();
        }

        public async Task<IEnumerable<Role>> GetRolesAsync()
        {
            return await _dbContext
                .Roles
                .ToListAsync();
        }

        public async Task RegisterAccountAsync(RegisterUserDto dto)
        {
            var users = await _dbContext
                .Users
                .ToListAsync();

            if (users.Any(x => string.Equals(x.Email, dto.Email, StringComparison.OrdinalIgnoreCase)))
            {
                throw new BadRequestException("User with specified email address already exists");
            }

            User newUser = new()
            {
                Email = dto.Email,
                RoleId = dto.RoleId,
            };

            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, dto.Password);
            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation(Message.MSG_CREATED, newUser.Id);
        }

        private static UserDto ToDto(User u)
        {
            return new UserDto()
            {
                Email = u.Email,
                RoleName = u.Role.Name
            };
        }
    }
}