﻿using System.ComponentModel;

namespace WebClientApp.Data
{
    public sealed class Facility
    {
        public Guid Id { get; set; }

        [DisplayName("Facility")]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? BuildingNumber { get; set; }

        public string? ApartmentNumber { get; set; }

        public string? StreetName { get; set; }
        public string City { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }
        public int FreeSpace { get; set; }
        public List<Guid>? Animals { get; set; }
    }
}