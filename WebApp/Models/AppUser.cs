using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class AppUser : IdentityUser
    {
        [Display(Name = "FullName")]
        public string FullName { get; set; } = string.Empty;
    }
}