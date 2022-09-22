﻿using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class User
    {
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Role")]
        public string Role { get; set; } = string.Empty;

        [Display(Name = "Token")]
        public string Token { get; set; } = string.Empty;

        [Display(Name = "SessionId")]
        public string SessionId { get; set; } = string.Empty;
    }
}