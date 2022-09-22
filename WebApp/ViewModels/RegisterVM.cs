using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password fields do not match")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Display(Name = "Date of birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "Role ID")]
        [Required(ErrorMessage = "RoleId is required")]
        public string RoleId { get; set; } = string.Empty;
    }
}