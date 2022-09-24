using System.ComponentModel.DataAnnotations;
using WebApp.Data;

namespace WebApp.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least {1} characters long")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least {1} characters long")]
        [Compare("Password", ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Role ID")]
        [Required(ErrorMessage = "RoleId is required")]
        public string RoleId { get; set; } = string.Empty;
    }
}