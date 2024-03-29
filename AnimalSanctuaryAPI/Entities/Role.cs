﻿using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Entities
{
    public sealed class Role
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; } = String.Empty;
    }
}