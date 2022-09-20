using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApp.Enums;

namespace WebApp.Models
{
    public class AnimalViewModel
    {
        [DisplayName("ID")]
        public Guid Id { get; set; }

        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Date of birth")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        public Sex? Sex { get; set; }

        [DisplayName("Health state")]
        public string? HealthState { get; set; }

        public Attitude? Attitude { get; set; }

        [DisplayName("Registration date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateCreated { get; set; }

        [DisplayName("Specie")]
        public string Specie { get; set; } = String.Empty;

        [DisplayName("Type")]
        public string Type { get; set; } = String.Empty;

        [DisplayName("Facility")]
        public string Facility { get; set; } = String.Empty;
    }
}