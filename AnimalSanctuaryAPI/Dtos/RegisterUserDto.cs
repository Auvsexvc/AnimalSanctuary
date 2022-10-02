using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class RegisterUserDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least {1} characters long")]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least {1} characters long")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public int RoleId { get; set; } = 1;
    }
}