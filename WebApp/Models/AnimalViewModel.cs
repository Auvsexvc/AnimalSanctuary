using WebApp.Data;
using WebApp.Enums;

namespace WebApp.Models
{
    public class AnimalViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public AnimalSpecie Specie { get; set; } = new AnimalSpecie();
        public AnimalType Type { get; set; } = new AnimalType();

        public Facility Facility { get; set; } = new Facility();
        public Guid SpecieId { get; set; }
        public Guid TypeId { get; set; }

        public Guid FacilityId { get; set; }
    }
}