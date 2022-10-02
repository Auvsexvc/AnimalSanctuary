using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebClientApp.Enums;
using WebClientApp.Validators;
using WebClientApp.ViewModels.Base;

namespace WebClientApp.Dtos
{
    public sealed class AnimalDto : ProfileImageFormFileBase
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