using AnimalSanctuaryAPI.Enums;
using AnimalSanctuaryAPI.Validators;
using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class AnimalDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DateMustNotBeFuture]
        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }
        public string? HealthState { get; set; }
        public Attitude? Attitude { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;

        [Required]
        [RequiredGuid]
        public Guid SpecieId { get; set; }

        [Required]
        [RequiredGuid]
        public Guid FacilityId { get; set; }
    }
}