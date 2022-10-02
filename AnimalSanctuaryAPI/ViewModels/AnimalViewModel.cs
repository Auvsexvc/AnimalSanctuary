using AnimalSanctuaryAPI.Enums;

namespace AnimalSanctuaryAPI.ViewModels
{
    public sealed class AnimalViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid? SpecieId { get; set; }

        public Guid? FacilityId { get; set; }
    }
}