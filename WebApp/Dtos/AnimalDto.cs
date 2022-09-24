using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApp.Enums;
using WebApp.Validators;

namespace WebApp.Dtos
{
    public class AnimalDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DisplayName("Date of birth")]
        [DateMustNotBeFuture]
        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        [DisplayName("Health state")]
        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        [DisplayName("Registration date")]
        public DateTime DateCreated { get; set; }

        [Required]
        [DisplayName("Specie")]
        public Guid SpecieId { get; set; }

        [Required]
        [DisplayName("Facility")]
        public Guid FacilityId { get; set; }
    }
}