using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos
{
    public class AnimalSpecieDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public Guid TypeId { get; set; }
    }
}