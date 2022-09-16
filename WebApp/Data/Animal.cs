using WebApp.Enums;

namespace WebApp.Data
{
    public class Animal
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        public DateTime DateCreated { get; set; }

        public string? Specie { get; set; }

        public string? Type { get; set; }

        public string? Facility { get; set; }
    }
}