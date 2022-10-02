using System.ComponentModel.DataAnnotations;

namespace WebClientApp.Dtos
{
    public sealed class AnimalSpecieDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public Guid TypeId { get; set; }
    }
}