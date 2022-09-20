using System.ComponentModel.DataAnnotations;

namespace WebApp.Dtos
{
    public class FacilityDto
    {
        [Required]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [Required]
        public string BuildingNumber { get; set; } = string.Empty;

        public string? ApartmentNumber { get; set; }

        [Required]
        public string StreetName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        public int MaxCapacity { get; set; }
    }
}