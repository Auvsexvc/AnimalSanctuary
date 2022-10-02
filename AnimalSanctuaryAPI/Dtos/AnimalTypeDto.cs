using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class AnimalTypeDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
    }
}