using System.ComponentModel.DataAnnotations;

namespace AnimalSanctuaryAPI.ViewModels
{
    public class FacilityViewModel
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public string? BuildingNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? StreetName { get; set; }
        public string? City { get; set; }

        [Phone]
        public string PhoneNumber { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }
        public int FreeSpace { get; set; }
        public List<Guid>? Animals { get; set; }
    }
}