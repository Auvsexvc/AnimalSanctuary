using AnimalSanctuaryAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class AnimalSpecieDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        [RequiredGuid]
        public Guid TypeId { get; set; }
    }
}