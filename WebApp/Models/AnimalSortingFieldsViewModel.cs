using System.ComponentModel;
using WebApp.Enums;

namespace WebApp.Models
{
    public class AnimalSortingFieldsViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Date of birth")]
        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        [DisplayName("Health state")]
        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        [DisplayName("Registration date")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Specie")]
        public string Specie { get; set; } = String.Empty;

        [DisplayName("Type")]
        public string Type { get; set; } = String.Empty;

        [DisplayName("Facility")]
        public string Facility { get; set; } = String.Empty;
    }
}