using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnimalSanctuaryAPI.Controllers
{
    [Authorize(Roles = "Administrator")]
    [Route("api/Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userDtos = await _accountService.GetAllAccountsAsync();

            return Ok(userDtos);
        }

        [HttpGet("Roles")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _accountService.GetRolesAsync();

            return Ok(roles);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto dto)
        {
            await _accountService.RegisterAccountAsync(dto);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            string token = await _accountService.GenereateJWTAsync(dto);

            return Ok(token);
        }
    }
}