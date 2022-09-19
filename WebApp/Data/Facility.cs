using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Data
{
    public class Facility
    {
        public Guid Id { get; set; }

        [DisplayName("Facility")]
        public string Name { get; set; } = String.Empty;

        public string? Description { get; set; }

        public string? BuildingNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? StreetName { get; set; }
        public string? City { get; set; }

        [Phone]
        public string? PhoneNumber { get; set; }

        public int MaxCapacity { get; set; }
        public int FreeSpace { get; set; }
        public List<Guid>? Animals { get; set; }
    }
}