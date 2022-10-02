using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Dtos
{
    public sealed class FacilityDto
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public string? BuildingNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? StreetName { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [MinLength(2)]
        public string City { get; set; } = string.Empty;

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Must be greater than or equal to {1}")]
        public int MaxCapacity { get; set; }
    }
}