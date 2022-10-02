using System.ComponentModel;
using WebClientApp.Enums;

namespace WebClientApp.Data
{
    public sealed class Animal
    {
        public Guid Id { get; set; }

        [DisplayName("Animal")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid SpecieId { get; set; }
        public string Specie { get; set; } = string.Empty;

        public Guid TypeId { get; set; }
        public string Type { get; set; } = string.Empty;

        public Guid FacilityId { get; set; }
        public string Facility { get; set; } = string.Empty;
    }
}