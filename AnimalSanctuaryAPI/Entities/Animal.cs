using AnimalSanctuaryAPI.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace AnimalSanctuaryAPI.Entities
{
    public sealed class Animal
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }
        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }
        public DateTime DateCreated { get; set; }

        public Guid SpecieId { get; set; }

        [Required]
        [JsonIgnore]
        [ForeignKey("SpecieId")]
        public AnimalSpecie Specie { get; set; } = new AnimalSpecie();

        public Guid FacilityId { get; set; }

        [Required]
        [JsonIgnore]
        [ForeignKey("FacilityId")]
        public Facility Facility { get; set; } = new Facility();
    }
}