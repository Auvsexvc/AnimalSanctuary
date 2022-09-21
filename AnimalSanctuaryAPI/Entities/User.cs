﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AnimalSanctuaryAPI.Entities
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        [ForeignKey("RoleId")]
        public virtual Role Role { get; set; } = null!;

        public int RoleId { get; set; }
    }
}