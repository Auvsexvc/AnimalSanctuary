using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApp.Data;

namespace WebApp.Models
{
    public class UpdateFacilityViewModel
    {
        [DisplayName("Facility ID")]
        public Guid Id { get; set; }

        [DisplayName("Facility")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Building number")]
        public string? BuildingNumber { get; set; }

        [DisplayName("apartment number(optional)")]
        public string? ApartmentNumber { get; set; }

        [DisplayName("Street name")]
        public string? StreetName { get; set; }

        [DisplayName("City")]
        public string City { get; set; } = String.Empty;

        [Phone]
        [DisplayName("Contact number")]
        public string? PhoneNumber { get; set; }

        [Required]
        public int MaxCapacity { get; set; }

        public int FreeSpace { get; set; }

        [DisplayName("Animals IDs")]
        public List<Guid>? AnimalsIds { get; set; }

        [DisplayName("Animals")]
        public IEnumerable<Animal>? Animals { get; set; }
    }
}