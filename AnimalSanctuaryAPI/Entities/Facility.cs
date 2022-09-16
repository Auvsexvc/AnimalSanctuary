using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.Entities
{
    public class Facility
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        [Required]
        public string BuildingNumber { get; set; } = string.Empty;

        public string? ApartmentNumber { get; set; }

        [Required]
        public string StreetName { get; set; } = string.Empty;

        [Required]
        public string City { get; set; } = string.Empty;

        [Required]
        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int MaxCapacity { get; set; }

        public List<Animal>? Animals { get; set; }
    }
}