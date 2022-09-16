using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Extensions
{
    public static class FacilityExtension
    {
        public static FacilityViewModel ToViewModel(this Facility data)
        {
            return new FacilityViewModel()
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                BuildingNumber = data.BuildingNumber,
                ApartmentNumber = data.ApartmentNumber,
                StreetName = data.StreetName,
                City = data.City,
                PhoneNumber = data.PhoneNumber,
                MaxCapacity = data.MaxCapacity,
                FreeSpace = data.MaxCapacity - (data.Animals != null ? data.Animals.Count : 0),
                Animals = data.Animals?.ConvertAll(x=>x.Id)
            };
        }

        public static Facility NewFromDto(this FacilityDto dto)
        {
            return new Facility()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                BuildingNumber = dto.BuildingNumber,
                ApartmentNumber = dto.ApartmentNumber,
                StreetName = dto.StreetName,
                City = dto.City,
                PhoneNumber = dto.PhoneNumber,
                MaxCapacity = dto.MaxCapacity
            };
        }

        public static Facility UpdateFromDto(this Facility data, FacilityDto dto)
        {
            data.Name = dto.Name;
            data.Description = dto.Description;
            data.BuildingNumber = dto.BuildingNumber;
            data.ApartmentNumber = dto.ApartmentNumber;
            data.StreetName = dto.StreetName;
            data.City = dto.City;
            data.PhoneNumber = dto.PhoneNumber;
            data.MaxCapacity = dto.MaxCapacity;

            return data;
        }
    }
}