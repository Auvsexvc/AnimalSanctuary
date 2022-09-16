using AnimalSanctuaryAPI.Dtos;
using AnimalSanctuaryAPI.Entities;
using AnimalSanctuaryAPI.ViewModels;

namespace AnimalSanctuaryAPI.Extensions
{
    public static class AnimalSpecieExtension
    {
        public static AnimalSpecieViewModel ToViewModel(this AnimalSpecie animalSpecie)
        {
            return new AnimalSpecieViewModel()
            {
                Id = animalSpecie.Id,
                Name = animalSpecie.Name,
                Description = animalSpecie.Description,
                TypeId = animalSpecie.TypeId,
                TypeName = animalSpecie.Type.Name
            };
        }

        public static AnimalSpecie NewFromDto(this AnimalSpecieDto dto, AnimalType type)
        {
            return new AnimalSpecie()
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Description = dto.Description,
                TypeId = type.Id,
                Type = type
            };
        }

        public static AnimalSpecie UpdateFromDto(this AnimalSpecie data, AnimalSpecieDto dto, AnimalType type)
        {
            data.Name = dto.Name;
            data.Description = dto.Description;
            data.TypeId = type.Id;
            data.Type = type;

            return data;
        }
    }
}