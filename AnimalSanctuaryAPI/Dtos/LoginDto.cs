using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class LoginDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}