using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos
{
    public class AnimalTypeDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}