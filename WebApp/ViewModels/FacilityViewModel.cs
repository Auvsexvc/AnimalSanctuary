using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebClientApp.Data;
using WebClientApp.Interfaces;
using WebClientApp.ViewModels.Base;

namespace WebClientApp.ViewModels
{
    public sealed class FacilityViewModel : ProfileImagePathBase, IBaseWithPictureViewModel
    {
        [DisplayName("Facility ID")]
        public Guid Id { get; set; }

        [DisplayName("Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Building number")]
        public string? BuildingNumber { get; set; }

        [DisplayName("apartment number(optional)")]
        public string? ApartmentNumber { get; set; }

        [DisplayName("Street name")]
        public string? StreetName { get; set; }

        [DisplayName("City")]
        public string City { get; set; } = string.Empty;

        [Phone]
        [DisplayName("Contact number")]
        public string? PhoneNumber { get; set; }

        [DisplayName("Max. capacity")]
        [Required]
        public int MaxCapacity { get; set; }

        [DisplayName("Free space")]
        public int FreeSpace { get; set; }

        public IEnumerable<Animal>? Animals { get; set; }
    }
}