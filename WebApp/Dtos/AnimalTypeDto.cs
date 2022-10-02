using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos
{
    public sealed class AnimalTypeDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}