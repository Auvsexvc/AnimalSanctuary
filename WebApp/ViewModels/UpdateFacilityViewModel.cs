using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebApp.Data;
using WebApp.Interfaces;
using WebApp.ViewModels.Base;

namespace WebApp.ViewModels
{
    public class UpdateFacilityViewModel : ProfileImageBase, IBaseViewModel
    {
        [DisplayName("Facility ID")]
        public Guid Id { get; set; }

        [DisplayName("Facility")]
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        [DisplayName("Building number")]
        public string? BuildingNumber { get; set; }

        [DisplayName("Apartment number(optional)")]
        public string? ApartmentNumber { get; set; }

        [DisplayName("Street")]
        public string? StreetName { get; set; }

        [Required]
        [DisplayName("City")]
        [MinLength(2)]
        public string City { get; set; } = string.Empty;

        [Phone]
        [DisplayName("Contact number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [DisplayName("Max. capacity")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int MaxCapacity { get; set; }

        [DisplayName("Free. space")]
        public int FreeSpace { get; set; }

        [DisplayName("Animals IDs")]
        public List<Guid>? AnimalsIds { get; set; }

        [DisplayName("Animals")]
        public IEnumerable<Animal>? Animals { get; set; }
    }
}