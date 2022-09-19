using WebApp.Enums;

namespace WebApp.Models
{
    public class FilterAnimalViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public string Specie { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;

        public string Facility { get; set; } = String.Empty;
    }
}