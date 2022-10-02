using System.ComponentModel;
using WebApp.Enums;

namespace WebApp.Data
{
    public sealed class Animal
    {
        public Guid Id { get; set; }

        [DisplayName("Animal")]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid SpecieId { get; set; }
        public string Specie { get; set; } = String.Empty;

        public Guid TypeId { get; set; }
        public string Type { get; set; } = String.Empty;

        public Guid FacilityId { get; set; }
        public string Facility { get; set; } = String.Empty;
    }
}