using System.ComponentModel.DataAnnotations;
using WebApp.Enums;

namespace WebApp.Dtos
{
    public class AnimalDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public Sex? Sex { get; set; }
        public string? HealthState { get; set; }
        public Attitude? Attitude { get; set; }
        public DateTime DateCreated { get; } = DateTime.Now;

        [Required]
        public Guid SpecieId { get; set; }

        [Required]
        public Guid FacilityId { get; set; }
    }
}
