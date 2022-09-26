﻿namespace WebApp.Models
{
    public class AccountModel
    {
        public string Id { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public DateTime? ValidTo { get; set; }

        public string SessionId { get; set; } = string.Empty;
    }
}