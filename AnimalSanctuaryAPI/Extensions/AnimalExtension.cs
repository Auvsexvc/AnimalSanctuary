using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Extensions
{
    public static class AnimalExtension
    {
        public static AnimalViewModel ToViewModel(this Animal animal)
        {
            return new AnimalViewModel()
            {
                Id = animal.Id,
                Name = animal.Name,
                Description = animal.Description,
                DateOfBirth = animal.DateOfBirth,
                Sex = animal.Sex,
                HealthState = animal.HealthState,
                Attitude = animal.Attitude,
                DateCreated = animal.DateCreated,
                FacilityId = animal.Facility.Id,
                Facility = animal.Facility.Name,
                SpecieId = animal.Specie.Id,
                Specie = animal.Specie.Name,
                TypeId = animal.Specie.TypeId,
                Type = animal.Specie.Type.Name
            };
        }

        public static Animal NewFromDto(this AnimalDto dto, AnimalSpecie specie, Facility facility)
        {
            return new Animal()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                DateOfBirth = dto.DateOfBirth,
                Sex = dto.Sex,
                HealthState = dto.HealthState,
                Attitude = dto.Attitude,
                DateCreated = dto.DateCreated,
                SpecieId = specie.Id,
                Specie = specie,
                FacilityId = facility.Id,
                Facility = facility
            };
        }

        public static Animal UpdateFromDto(this Animal data, AnimalDto dto, AnimalSpecie specie, Facility facility)
        {
            data.Name = dto.Name;
            data.Description = dto.Description;
            data.DateOfBirth = dto.DateOfBirth;
            data.Sex = dto.Sex;
            data.HealthState = dto.HealthState;
            data.Attitude = dto.Attitude;
            data.SpecieId = specie.Id;
            data.Specie = specie;
            data.FacilityId = facility.Id;
            data.Facility = facility;

            return data;
        }
    }
}