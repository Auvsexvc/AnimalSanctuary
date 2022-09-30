using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApp.ViewModels.Base;

namespace WebApp.Dtos
{
    public class FacilityDto : ProfileImageFormFileBase
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [DisplayName("Building number")]
        public string? BuildingNumber { get; set; } = string.Empty;

        [DisplayName("Apartment number")]
        public string? ApartmentNumber { get; set; }

        [DisplayName("Street")]
        public string? StreetName { get; set; } = string.Empty;

        [Required]
        [Phone]
        [DisplayName("Contact number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string City { get; set; } = string.Empty;

        [Required]
        [DisplayName("Max. capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than or equal to {1}")]
        public int MaxCapacity { get; set; }
    }
}